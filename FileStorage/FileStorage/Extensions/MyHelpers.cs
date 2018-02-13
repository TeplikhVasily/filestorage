using DbStorage.NHibernate.Repositiry;
using System.Web.Mvc;

namespace FileStorage.Extensions
{
    public static class MyHelpers
    {
        public static MvcHtmlString ListerFlat(this HtmlHelper html, string[] items)
        {
            var ul = new TagBuilder("ul");
            foreach (var item in items)
            {
                var li = new TagBuilder("li");
                li.InnerHtml = item;

                ul.InnerHtml += li;
            }
            return MvcHtmlString.Create(ul.ToString());
        }

        //Кнопка
        public static MvcHtmlString Submit(this HtmlHelper html, string value)
        {
            var button = new TagBuilder("input");
            button.MergeAttribute("type", "submit");
            button.AddCssClass("btn btn-success");
            if (!string.IsNullOrWhiteSpace(value))
            {
                button.MergeAttribute("value", value);
            }

            return MvcHtmlString.Create(button.ToString());
        }

        public static MvcHtmlString FIO(this HtmlHelper html)
        {
            var name = html.ViewContext.HttpContext.User.Identity.Name;
            var rep = new NHUserRepository();
            var user = rep.GetByLogin(name);
            return MvcHtmlString.Create($"😠 {user.FirstName} {user.LastName}");
        }
    }
}