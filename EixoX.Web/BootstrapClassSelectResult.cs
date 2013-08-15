using EixoX.Data;
using EixoX.Html;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Text;

using System.Reflection;

namespace EixoX.Web
{
    /// <summary>
    /// Represents a result of a select made with some parameters, to be able to build automatic filters
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BootstrapClassSelectResult<T> : IEnumerable<T>
    {
        /// <summary>
        /// Search Filter, the term to filter all the records with
        /// </summary>
        private Pair<string, string> _SearchFilter;
        /// <summary>
        /// Page ordinal, to set what is the current result's page
        /// </summary>
        private Pair<string, int> _PageOrdinal;
        /// <summary>
        /// Sets the size of the pages
        /// </summary>
        private Pair<string, int> _PageSize;
        /// <summary>
        /// Column to be used in the Order By statement
        /// </summary>
        private Pair<string, int> _OrderBy;
        /// <summary>
        /// Ascending or Descending
        /// </summary>
        private Pair<string, SortDirection> _SortDirection;

        private Dictionary<string, string> DefaultParameterKeys;

        /// <summary>
        /// Name of the form, used for rendering stuff
        /// </summary>
        private string FormName;

        /// <summary>
        /// Query's result
        /// </summary>
        public ClassSelectResult<T> Result { get; private set; }

        /// <summary>
        /// Populates the default's keys parameter dictionary
        /// </summary>
        private void PopulateDefaultParameterKeys()
        {
            this.DefaultParameterKeys = new Dictionary<string, string>() 
            {
                { "Filter", this.FormName + "Filter"},
                { "PageOrdinal", this.FormName + "PageOrdinal" },
                { "PageSize", this.FormName + "PageSize" }, 
                { "OrderBy", this.FormName + "OrderBy" },
                { "SortDirection", this.FormName + "SortDirection" }
            };
        }

        private static Pair<string, G> GetValueFromCollection<G>(NameValueCollection form, string name, G defaultG)
        {
            string value = form[name];

            if (string.IsNullOrEmpty(value))
                return new Pair<string, G>(name, defaultG);
            else
            {
                if (typeof(G).IsEnum)
                    return new Pair<string, G>(name, (G)Enum.Parse(typeof(G), value));
                else
                    return new Pair<string, G>(name, (G)Convert.ChangeType(value, typeof(G), System.Globalization.CultureInfo.CurrentUICulture));
            }
        }

        public BootstrapClassSelectResult(ClassStorage<T> storage, string formName, NameValueCollection form)
            : this(storage, formName, form, null) { }

        public BootstrapClassSelectResult(ClassStorage<T> storage, string formName, NameValueCollection form, ClassFilter filter)
        {
            this.FormName = formName;
            PopulateDefaultParameterKeys();
            this._SearchFilter = GetValueFromCollection<string>(form, this.DefaultParameterKeys["Filter"], null);
            this._PageOrdinal = GetValueFromCollection<int>(form, this.DefaultParameterKeys["PageOrdinal"], 0);
            this._PageSize = GetValueFromCollection<int>(form, this.DefaultParameterKeys["PageSize"], 20);
            this._OrderBy = GetValueFromCollection<int>(form, this.DefaultParameterKeys["OrderBy"], -1);
            this._SortDirection = GetValueFromCollection<SortDirection>(form, this.DefaultParameterKeys["SortDirection"], SortDirection.Ascending);

            ClassSelect<T> select = null;

            if (string.IsNullOrEmpty(_SearchFilter.Value))
            {
                select = storage.Select();

                if (filter != null)
                {
                    select.Where(filter);
                }
            }
            else
            {
                select = storage.Search(_SearchFilter.Value);

                if (filter != null)
                {
                    select.And(filter);
                }
            }

            if (_OrderBy.Value >= 0)
            {
                select.OrderBy(_OrderBy.Value, _SortDirection.Value);
            }
            else
            {
                select.OrderBy(0, SortDirection.Descending);
            }

            select.Page(_PageSize.Value, _PageOrdinal.Value);

            this.Result = new ClassSelectResult<T>(select);
        }

        public void RenderAll(TextWriter writer, params int[] valores)
        {
            RenderSearchBox(writer);
            RenderOrderBy(writer);
            RenderPageDropDown(writer);
            RenderPagination(writer);
            RenderJSControls(writer);
        }

        public void RenderSearchBox(TextWriter writer)
        {
            HtmlComposite searchBox = new HtmlComposite("div", new HtmlAttribute("class", "input-append input-prepend"));

            searchBox.AppendSimple(
                    "span",
                    "Filter",
                    new HtmlAttribute("class", "add-on"))
                .AppendStandalone("input",
                new HtmlAttribute("type", "text"),
                new HtmlAttribute("placeholder", "Search"),
                new HtmlAttribute("name", _SearchFilter.Key),
                new HtmlAttribute("class", "input-small"),
                new HtmlAttribute("value", _SearchFilter.Value))
                .AppendComposite(
                    "button",
                    new HtmlAttribute("type", "text"),
                    new HtmlAttribute("class", "btn btn-primary"))
                .AppendSimple("i",
                    "",
                    new HtmlAttribute("class", "icon-search icon-white"));

            searchBox.Write(writer);
        }

        public void RenderPageDropDown(TextWriter writer)
        {
            HtmlComposite controlDiv = new HtmlComposite("div", new HtmlAttribute("class", "input-prepend"));
            controlDiv.AppendSimple("span", "Page Size", new HtmlAttribute("class", "add-on"));
            HtmlComposite dropdown = controlDiv.AppendComposite(
                "select",
                new HtmlAttribute("name", _PageSize.Key),
                new HtmlAttribute("id", _PageSize.Key),
                new HtmlAttribute("class", "input-mini changeReload"));

            for (int i = 10; i <= 50; i += 10)
                dropdown.AppendHtmlOption(i, i.ToString(), i == _PageSize.Value);

            controlDiv.Write(writer);
        }

        public void RenderOrderBy(TextWriter writer)
        {
            HtmlComposite divOrderBy = new HtmlComposite(
                "div",
                new HtmlAttribute("class", "input-prepend input-append"));

            divOrderBy.AppendSimple(
                "span",
                "Order by",
                new HtmlAttribute("class", "add-on"));

            HtmlComposite selectOrderBy = divOrderBy.AppendComposite(
                "select",
                new HtmlAttribute("name", _OrderBy.Key),
                new HtmlAttribute("id", _OrderBy.Key),
                new HtmlAttribute("class", "changeReload"));

            EixoX.Data.DatabaseAspect<T> aspect = EixoX.Data.DatabaseAspect<T>.Instance;
            for (int i = 0; i < aspect.Count; i++)
                selectOrderBy.AppendHtmlOption(i, aspect[i].Name, i == _OrderBy.Value);
            
            HtmlComposite selectDirection = divOrderBy.AppendComposite(
                "select",
                new HtmlAttribute("name", _SortDirection.Key),
                new HtmlAttribute("id", _SortDirection.Key),
                new HtmlAttribute("class", "input-small  changeReload"));

            if (_SortDirection.Value == SortDirection.Ascending)
            {
                selectDirection.AppendSimple(
                        "option",
                        SortDirection.Ascending,
                        new HtmlAttribute("value", SortDirection.Ascending),
                        new HtmlAttribute("selected", "selected"));

                selectDirection.AppendSimple(
                        "option",
                        SortDirection.Descending,
                        new HtmlAttribute("value", SortDirection.Descending));
            }
            else
            {
                selectDirection.AppendSimple(
                        "option",
                        SortDirection.Ascending,
                        new HtmlAttribute("value", SortDirection.Ascending));

                selectDirection.AppendSimple(
                        "option",
                        SortDirection.Descending,
                        new HtmlAttribute("value", SortDirection.Descending),
                        new HtmlAttribute("selected", "selected"));
            }

            divOrderBy.Write(writer);
        }

        public void RenderPagination(TextWriter writer)
        {
            HtmlComposite paginationDiv = new HtmlComposite(
                "div",
                new HtmlAttribute("class", "pagination pull-right"));

            paginationDiv.AppendComposite(
                "input",
                new HtmlAttribute("type", "hidden"),
                new HtmlAttribute("name", _PageOrdinal.Key),
                new HtmlAttribute("id", _PageOrdinal.Key),
                new HtmlAttribute("value", Result.PageOrdinal));

            HtmlComposite paginationUl = new HtmlComposite(
                "ul",
                new HtmlAttribute("class", ""));

            paginationUl.AppendComposite(
                "li",
                new HtmlAttribute("class", ""))
                .AppendSimple(
                    "a",
                    "First",
                    new HtmlAttribute("id", "firstPage"),
                    new HtmlAttribute("href", "#"));

            //Previous page button
            paginationUl.AppendComposite(
                "li",
                new HtmlAttribute("class", Result.HasPreviousPages ? "" : "disabled"))
                .AppendSimple(
                    "a",
                    "Previous",
                    new HtmlAttribute("id", "previousPage"),
                    Result.HasPreviousPages ? new HtmlAttribute("href", "#") : new HtmlAttribute());

            //Records found
            paginationUl.AppendComposite(
                "li",
                new HtmlAttribute("class", ""))
                .AppendSimple(
                    "a",
                    Result.RecordCount + " records. Page " + (Result.PageOrdinal + 1) + " of " + Result.PageCount,
                    new HtmlAttribute());

            //Next page button
            paginationUl.AppendComposite(
                "li",
                new HtmlAttribute("class", Result.HasMorePages ? "" : "disabled"))
                .AppendSimple(
                    "a",
                    "Next",
                    new HtmlAttribute("id", "nextPage"),
                    Result.HasMorePages ? new HtmlAttribute("href", "#") : new HtmlAttribute());

            //Last page button
            paginationUl.AppendComposite(
                "li",
                new HtmlAttribute("class", ""))
                .AppendSimple(
                    "a",
                    "Last",
                    new HtmlAttribute("id", "lastPage"),
                    new HtmlAttribute("href", "#"));

            paginationDiv.Children.AddLast(paginationUl);
            paginationDiv.Write(writer);
        }

        private void RenderJSControls(TextWriter writer)
        {
            StringBuilder builder = new StringBuilder();
            writer.Write("<script type=\"text/javascript\">");

            //PAGINATION
            //FirstPage
            writer.Write("$('#firstPage').click(function () {$('#" + this.FormName + "PageOrdinal').val(0);$(this).closest('form').submit();});");
            //Previous Page
            builder.Length = 0;
            builder.Append("$('#previousPage').click(function () {$('#" + this.FormName + "PageOrdinal').val(");
            builder.Append(Result.PageOrdinal - 1);
            builder.Append(");$(this).closest('form').submit();});");
            writer.Write(Result.HasPreviousPages ? builder.ToString() : "");
            //Next Page
            builder.Length = 0;
            builder.Append("$('#nextPage').click(function () {$('#" + this.FormName + "PageOrdinal').val(");
            builder.Append(Result.PageOrdinal + 1);
            builder.Append(");$(this).closest('form').submit();});");
            writer.Write(Result.HasMorePages ? builder.ToString() : "");
            //Last Page
            builder.Length = 0;
            builder.Append("$('#lastPage').click(function () {$('#" + this.FormName + "PageOrdinal').val(");
            builder.Append(Result.PageCount - 1);
            builder.Append(");$(this).closest('form').submit();});");
            writer.Write(builder.ToString());

            //DROPDOWNS
            builder.Length = 0;
            builder.Append("$('.changeReload').change(function() { $('#");
            builder.Append(_PageOrdinal.Key);
            builder.Append("').val(0); $(this).closest('form').submit()});");
            writer.Write(builder.ToString());

            writer.Write("</script>");
        }

        public IEnumerator<T> GetEnumerator()
        {
            return Result.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return Result.GetEnumerator();
        }
    }
}
