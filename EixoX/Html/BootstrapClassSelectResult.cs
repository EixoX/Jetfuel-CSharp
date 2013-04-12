using EixoX.Data;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Text;
using EixoX.Html;

namespace EixoX.Html
{
    public class BootstrapClassSelectResult<T>
        : EixoX.Data.ClassSelectResult<T>
    {
        private static string searchBoxName;
        private static string pageFieldName;
        private static string pageSizeName;
        private static string orderByName;
        private static string sortDirectionName;
        private static string filter;


        private static ClassSelect<T> BuildSelect(ClassStorage<T> storage, string formName, NameValueCollection form)
        {
            searchBoxName = formName + "filter";
            pageFieldName = formName + "page";
            pageSizeName = formName + "pageSize";
            orderByName = formName + "orderBy";
            sortDirectionName = formName + "sortDirection";
            filter = form[searchBoxName] ?? "";


            ClassSelect<T> search = storage.Search(form[searchBoxName])
                .Page(Convert.ToInt32(form[pageSizeName]), Convert.ToInt32(form[pageFieldName]));


            int orderByOrdinal = Convert.ToInt32(form[orderByName]);

            if (orderByOrdinal > 0)
            {
                SortDirection sortDirection = (SortDirection)Convert.ToInt32(form[sortDirectionName]);

                search.OrderBy(orderByOrdinal, sortDirection);

            }
            return search;
        }

        public BootstrapClassSelectResult(ClassStorage<T> storage, string formName, NameValueCollection form)
            : base(BuildSelect(storage, formName, form)) { }

        public void RenderAll(TextWriter writer, params int[] valores)
        {
        }

        public void RenderSearchBox(TextWriter writer)
        {
            HtmlComposite searchForm = new HtmlComposite(
                "form",
                new HtmlAttribute("class", "form-search")
                );
            HtmlComposite searchDiv = new HtmlComposite(
                "div",
                new HtmlAttribute("class", "input-append"));
            
            HtmlStandalone searchField = new HtmlStandalone(
                "input",
                new HtmlAttribute("type", "text"),
                new HtmlAttribute("placeholder", "Search"),
                new HtmlAttribute("name", searchBoxName),
                new HtmlAttribute("class", "search-query input-xlarge"),
                new HtmlAttribute("value", filter));

            HtmlComposite searchButton = new HtmlComposite(
                "button",
                new HtmlAttribute("type", "text"),
                new HtmlAttribute("class", "btn btn-primary"));

            HtmlComposite searchIcon = new HtmlComposite(
                "i",
                new HtmlAttribute("class", "icon-search icon-white"));

            searchButton.Children.AddLast(searchIcon);
            searchDiv.Children.AddLast(searchField);
            searchDiv.Children.AddLast(searchButton);
            searchForm.Children.AddLast(searchDiv);
            searchForm.Write(writer);

            /*
                <input type="text" name="filter" class="search-query" value="@Request.QueryString["filter"]" />
                <button class="btn btn-primary">
                    <i class="icon-search icon-white"></i>
                </button>
             */
        }
    }
}
