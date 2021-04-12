using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WINTEX.Models.ViewModels;

namespace WINTEX.Infrastructure
{
    //target div element, using html attribute "page-info-"
    [HtmlTargetElement("div", Attributes = "page-info")]
    public class PaginationTagHelper : TagHelper
    {
        private IUrlHelperFactory urlInfo;
        public PaginationTagHelper(IUrlHelperFactory uhf)
        {
            urlInfo = uhf;
        }


        //dictionary (key value pairs) that we are creating
        [HtmlAttributeName(DictionaryAttributePrefix = "page-url-")]
        public Dictionary<string, object> KeyValuePairs { get; set; } = new Dictionary<string, object>();

        [HtmlAttributeNotBound]
        [ViewContext]
        public ViewContext ViewContext { get; set; }

        //properties to be set through html attributes
        
        public int NumPages {get;set;}
        public int CurrentPage { get; set; }
        public bool PageClassesEnabled { get; set; } = false;
        public string PageClass { get; set; }
        public string PageClassNormal { get; set; }
        public string PageClassSelected { get; set; }


        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            //initialize IUrlHelper, with our private IUrlHelperFactory urlInfo property
            IUrlHelper urlHelp = urlInfo.GetUrlHelper(ViewContext);

            //build a div tag
            TagBuilder finishedTag = new TagBuilder("div");

            //build a tag for each page there is
            for (int i = 1; i <= NumPages; i++)
            {
                TagBuilder individualTag = new TagBuilder("a");
                KeyValuePairs["pageNum"] = i;
                //assign href content with routing information
                individualTag.Attributes["href"] = urlHelp.Action("Index", KeyValuePairs);
                //if we are using page classes, then assign the tag a class appropriately
                if (PageClassesEnabled)
                {
                    individualTag.AddCssClass(PageClass);
                    //if iterating over the current page, assign the PageClassSelected styling. Otherwise, use PageClassNormal styling
                    individualTag.AddCssClass(i == CurrentPage ? PageClassSelected : PageClassNormal);
                }
                //append page number to tag innerhtml
                individualTag.InnerHtml.AppendHtml(i.ToString());
                //append tag to outer div tag innerhtml
                finishedTag.InnerHtml.AppendHtml(individualTag);
            }
            //append div's innerhtml to the page content
            output.Content.AppendHtml(finishedTag.InnerHtml);
        }
    }
}
