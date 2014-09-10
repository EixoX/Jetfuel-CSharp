using EixoX.Data;
using EixoX.Formatters;
using EixoX.Html;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace EixoX.Web
{
    public class BootstrapQueryHelper
    {
        private readonly List<BootstrapQueryHelperColumn> columns = new List<BootstrapQueryHelperColumn>();
        public string OrderBy { get; set; }
        public SortDirection OrderByDirection { get; set; }
        public int PageSize { get; set; }
        public int PageOrdinal { get; set; }
        public List<BootstrapQueryHelperColumn> Columns { get { return this.columns; } }
        public string OnClickFormatString { get; set; }
        public string[] OnClickColumns { get; set; }
        public long RecordCount { get; private set; }

        public BootstrapQueryHelper()
        {
            this.PageSize = 30;
        }

        public BootstrapQueryHelperColumn AddColumn(string name, string caption, Formatter formatter)
        {
            BootstrapQueryHelperColumn col = new BootstrapQueryHelperColumn()
            {
                Name = name,
                Caption = caption,
                Formatter = formatter
            };

            this.columns.Add(col);
            return col;
        }

        public void Parse(NameValueCollection data)
        {
            this.OrderBy = data["orderBy"];
            string direction = data["orderByDirection"];
            if (!string.IsNullOrEmpty(direction))
            {
                this.OrderByDirection = (SortDirection)Enum.Parse(typeof(SortDirection), direction);
            }
            for (int i = 0; i < columns.Count; i++)
            {
                columns[i].Filter = data[columns[i].Name];
            }
            string page = data["page"];
            if (!string.IsNullOrEmpty(page))
            {
                this.PageOrdinal = int.Parse(page);
            }
            string pageSize = data["pageSize"];
            if (!string.IsNullOrEmpty(pageSize))
            {
                this.PageSize = int.Parse(pageSize);
            }
        }

        private HtmlComposite BuildTableBody<T>(ClassSelectResult<T> result)
        {

            ClassSchema<T> aspect = ClassSchema<T>.Instance;

            HtmlComposite tbody = new HtmlComposite("tbody");

            AspectMember[] onclickMembers =
                OnClickColumns != null && OnClickColumns.Length > 0 ?
                new AspectMember[OnClickColumns.Length] : null;

            if (onclickMembers != null)
                for (int i = 0; i < onclickMembers.Length; i++)
                    onclickMembers[i] = aspect[OnClickColumns[i]];

            object[] onclickValues =
                OnClickColumns != null && OnClickColumns.Length > 0 ?
                new object[OnClickColumns.Length] : null;

            AspectMember[] members = new AspectMember[columns.Count];
            for (int i = 0; i < members.Length; i++)
                members[i] = aspect[columns[i].Name];

            foreach (T child in result)
            {
                HtmlComposite tr = tbody.AppendComposite("tr");

                if (!string.IsNullOrEmpty(this.OnClickFormatString))
                {
                    if (onclickValues != null)
                    {
                        for (int i = 0; i < onclickValues.Length; i++)
                            onclickValues[i] = onclickMembers[i].GetValue(child);

                        tr
                            .AppendAttribute("style", "cursor:pointer")
                            .AppendAttribute("onclick", string.Format(this.OnClickFormatString, onclickValues));
                    }
                    else
                    {
                        tr
                            .AppendAttribute("style", "cursor:pointer")
                            .AppendAttribute("onclick", this.OnClickFormatString);
                    }
                }


                for (int i = 0; i < members.Length; i++)
                {
                    BootstrapQueryHelperColumn col = columns[i];

                    HtmlComposite td = tr.AppendComposite("td");
                    if (!string.IsNullOrEmpty(col.CssStyle))
                    {
                        td.AppendAttribute("style", col.CssStyle);
                    }
                    if (!string.IsNullOrEmpty(col.CssClass))
                    {
                        td.AppendAttribute("class", col.CssClass);
                    }

                    object value = members[i].GetValue(child);
                    string content = col.Formatter.Format(value, CultureInfo.CurrentUICulture);

                    if (col.IsHtmlRaw)
                        td.Children.AddLast(new HtmlRaw(content));
                    else
                        td.AppendText(content);
                }
            }

            return tbody;
        }

        private HtmlComposite BuildTableFooter<T>(ClassSelectResult<T> result)
        {
            HtmlComposite footer = new HtmlComposite("tfoot");

            HtmlComposite footerDiv = footer
                .AppendComposite("tr")
                .AppendComposite("td", new HtmlAttribute("colspan", "100"))
                .AppendComposite("div", new HtmlAttribute("class", "row-fluid"));

            if (result.RecordCount == 0)
            {
                footerDiv.AppendComposite("div", new HtmlAttribute("class", "span6"))
                .AppendText("None, nothing, nadia");
            }
            else
            {
                footerDiv.AppendComposite("div", new HtmlAttribute("class", "span6"))
                .AppendText(string.Format("Showing {0:N0} to {1:N0} of {2:N0}", result.FirstRecordOrdinal, result.LastRecordOrdinal, result.RecordCount));
            }
            

            HtmlComposite pagingList = footerDiv.AppendComposite("div", new HtmlAttribute("class", "span6"))
                .AppendComposite("div", new HtmlAttribute("class", "pull-right"))
                .AppendComposite("ul", new HtmlAttribute("class", "pagination"));


            pagingList
                .AppendComposite("li", new HtmlAttribute("class", result.PageCount > 1 && result.PageOrdinal > 0 ? "" : "disabled"))
                .AppendSimple("a", "«",
                new HtmlAttribute("href", "javascript:void(0)"),
                new HtmlAttribute("onclick", "EixoX.pageGrid(this,'0')"));

            pagingList
                    .AppendComposite("li", new HtmlAttribute("class", result.PageCount > 1 && result.PageOrdinal > 0 ? "" : "disabled"))
                    .AppendSimple("a", "<",
                    new HtmlAttribute("href", "javascript:void(0)"),
                    new HtmlAttribute("onclick", string.Concat("EixoX.pageGrid(this,'", result.PageOrdinal - 1, "')")));

            pagingList
                .AppendComposite("li")
                .AppendComposite(
                "button",
                new HtmlAttribute("class", "btn btn-default"),
                new HtmlAttribute("type", "submit"))
                .AppendText("pages: " + result.PageCount);

            pagingList
                    .AppendComposite("li", new HtmlAttribute("class", result.PageCount > 1 && result.PageOrdinal < (result.PageCount - 1) ? "" : "disabled"))
                    .AppendSimple("a", ">",
                    new HtmlAttribute("href", "javascript:void(0)"),
                    new HtmlAttribute("onclick", string.Concat("EixoX.pageGrid(this,'", result.PageOrdinal + 1, "')")));

            pagingList
                .AppendComposite("li", new HtmlAttribute("class", result.PageCount > 1 && result.PageOrdinal < (result.PageCount - 1) ? "" : "disabled"))
                .AppendSimple("a", "»",
                new HtmlAttribute("href", "javascript:void(0)"),
                new HtmlAttribute("onclick", string.Concat("EixoX.pageGrid(this,'", result.PageCount - 1, "')")));

            return footer;
        }

        private HtmlComposite BuildTableHeader<T>()
        {
            HtmlComposite thead = new HtmlComposite("thead");

            HtmlComposite thead_tr = thead.AppendComposite("tr");

            foreach (BootstrapQueryHelperColumn col in this.columns)
            {
                HtmlComposite th = thead_tr.AppendComposite("th");
                HtmlComposite tha = th.AppendComposite("a");

                tha.AppendText(col.Caption + "   ");
                tha.AppendAttribute("href", "javascript:void(0)");

                if (col.Name.Equals(this.OrderBy, StringComparison.OrdinalIgnoreCase))
                {
                    if (this.OrderByDirection == SortDirection.Ascending)
                    {
                        tha.AppendSimple(
                            "span",
                            "",
                            new HtmlAttribute("class", "fa fa-sort-asc"));


                        tha.AppendAttribute("onclick", string.Concat("EixoX.sortGrid(this,'", col.Name, "','", SortDirection.Descending, "')"));
                    }
                    else
                    {
                        tha.AppendSimple(
                            "span",
                            "",
                            new HtmlAttribute("class", "fa fa-sort-desc"));

                        tha.AppendAttribute("onclick", string.Concat("EixoX.sortGrid(this,'", col.Name, "','", SortDirection.Ascending, "')"));
                    }
                }
                else
                {
                    tha.AppendSimple(
                            "span",
                            "",
                            new HtmlAttribute("class", "fa fa-sort"));

                    tha.AppendAttribute("onclick", string.Concat("EixoX.sortGrid(this,'", col.Name, "','", SortDirection.Ascending, "')"));
                }
            }

            HtmlComposite tr2 = thead.AppendComposite("tr");
            foreach (BootstrapQueryHelperColumn col in this.columns)
            {
                HtmlComposite th2 = tr2.AppendComposite(
                    "td",
                    new HtmlAttribute("style", "padding:0px")
                    );

                th2.AppendStandalone(
                    "input",
                    new HtmlAttribute("type", "text"),
                    new HtmlAttribute("placeholder", "filtrar"),
                    new HtmlAttribute("name", col.Name),
                    new HtmlAttribute("value", col.Filter),
                    new HtmlAttribute("style", "margin:0px; border-radius:0px; display:block; width:94%; border:none;"));
            }

            return thead;
        }

        private ClassSelectResult<T> ExecuteSelect<T>(ClassSelect<T> select)
        {
            if (!string.IsNullOrEmpty(this.OrderBy))
            {
                select.OrderBy(this.OrderByDirection, this.OrderBy);
            }

            select.Page(this.PageSize, this.PageOrdinal);

            ClassFilterExpression filter = null;
            foreach (BootstrapQueryHelperColumn col in this.columns)
            {
                if (!string.IsNullOrEmpty(col.Filter))
                {
                    filter = filter == null ?
                        new ClassFilterExpression(select.Aspect, col.Name, FilterComparison.Like, "%" + col.Filter.Replace(" ", "%") + "%") :
                        filter.And(col.Name, FilterComparison.Like, "%" + col.Filter.Replace(" ", "%") + "%");
                }
            }
            if (filter != null)
            {
                select.And(filter);
            }

            ClassSelectResult<T> result = select.ToResult();
            this.RecordCount = result.RecordCount;
            return result;
        }

        private void AppendUnrelatedFilters(HtmlComposite composite, Aspect aspect, NameValueCollection parameters)
        {
            foreach (string key in parameters.AllKeys)
            {
                if (
                    !"orderBy".Equals(key, StringComparison.OrdinalIgnoreCase) &&
                    !"orderByDirection".Equals(key, StringComparison.OrdinalIgnoreCase) &&
                    !"page".Equals(key, StringComparison.OrdinalIgnoreCase) &&
                    !"pageSize".Equals(StringComparison.OrdinalIgnoreCase) &&
                    !aspect.HasMember(key))
                {
                    composite.AppendStandalone("input",
                        new HtmlAttribute("type", "hidden"),
                        new HtmlAttribute("name", key),
                        new HtmlAttribute("value", parameters[key]));
                }
            }
        }

        public HtmlComposite Execute<T>(ClassSelect<T> select)
        {
            return Execute(select, null);
        }

        public HtmlComposite Execute<T>(ClassSelect<T> select, NameValueCollection parameters)
        {
            ClassSelectResult<T> result = ExecuteSelect<T>(select);

            HtmlComposite form = new HtmlComposite(
                "form",
                new HtmlAttribute("method", "get"));

            form.AppendStandalone("input",
                new HtmlAttribute("type", "hidden"),
                new HtmlAttribute("name", "orderBy"),
                new HtmlAttribute("value", this.OrderBy));

            form.AppendStandalone("input",
                new HtmlAttribute("type", "hidden"),
                new HtmlAttribute("name", "orderByDirection"),
                new HtmlAttribute("value", this.OrderByDirection));

            form.AppendStandalone("input",
                new HtmlAttribute("type", "hidden"),
                new HtmlAttribute("name", "page"),
                new HtmlAttribute("value", "0"));

            if (parameters != null)
            {
                AppendUnrelatedFilters(form, select.Aspect, parameters);
            }

            HtmlComposite table = form.AppendComposite(
                "table",
                new HtmlAttribute("class", "table table-condensed table-striped table-bordered table-hover")
                );

            table.Children.AddLast(BuildTableHeader<T>());
            table.Children.AddLast(BuildTableBody<T>(result));
            table.Children.AddLast(BuildTableFooter<T>(result));

            return form;
        }

    }
}
