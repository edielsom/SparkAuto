using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using SparkAuto.Models;

namespace SparkAuto.TagHelpers
{
    [HtmlTargetElement("div", Attributes = "page-model")]
    public class PageLinkTagHelper : TagHelper
    {
        private IUrlHelperFactory urlHelperFactory;

        public PageLinkTagHelper(IUrlHelperFactory helperFactory)
        {
            this.urlHelperFactory = helperFactory;
        }

        [ViewContext]
        //[HtmlAttributesNoteBound]
        public ViewContext  ViewContext { get; set; }
        public PageingInfo  pageModel { get; set; }
        public string PageAction { get; set; }
        public string PageClass { get; set; }
        public string PageClassNormal { get; set; }
        public string PageClassSelect { get; set; }

        public override void Process(TagHelperContext context,TagHelperOutput output)
        {
            IUrlHelper urlHelper = urlHelperFactory.GetUrlHelper(ViewContext);
            TagBuilder result = new TagBuilder("div");

            for (int i = 1; i <= pageModel.TotalItems; i++)
            {
                //Pega a tag âncoroa (a) e cria uma instância
                TagBuilder tag = new TagBuilder("a");
                //Pega a url.
                string url = pageModel.UrlParam.Replace(":", i.ToString());
                //Pega a url e insere na tag css href.
                tag.Attributes["href"] = url;

                //Adiciona as classes na tag âncora.
                tag.AddCssClass(PageClass);
                tag.AddCssClass(i == pageModel.CurrentPage ? PageClassSelect : PageClassNormal);
              
                //embute as informações html dentro da tag âncora.
                tag.InnerHtml.Append(i.ToString());

                //Insere as informações dentro da div;
                result.InnerHtml.AppendHtml(tag);
            }
            output.Content.AppendHtml(result.InnerHtml);
        }
    }
}
