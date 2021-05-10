using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace MemberShipApp.Extensions
{
	public static class ModelStateExtensions
	{
		public static IEnumerable<ModelErrorCollection> GetErrorsAsGroupedList(this ModelStateDictionary modelState)
		{
			return modelState.Select(x => x.Value.Errors).Where(list => list.Any()).ToList();
		}

		public static IEnumerable<ModelError> GetErrorsAsFlatList(this ModelStateDictionary modelState)
		{
			return modelState.GetErrorsAsGroupedList().SelectMany(s => s);
		}

		// Bug 5755 -- This was returning a string and the break was being displayed. What we really wanted was an html encoded string 
		// that makes the break work properly
		public static HtmlString GetErrorsAsBRList(this ModelStateDictionary modelState)
		{
			return new HtmlString(string.Join("<br/>", modelState.GetErrorsAsFlatList().Select(e => e.ErrorMessage)));
		}

		//public static ModelStateDictionary GetNewModelStateFromList(this ModelStateDictionary modelState, List<string> list)
		//{
		//	ModelStateDictionary msd = new ModelStateDictionary();
		//	foreach (var item in modelState.Where(ms => list.Contains(ms.Key)))
		//	{
		//		msd.AddModelError(string.Empty, item.Value);
		//	}
		//	return msd;
		//}

		public static List<string> GetErrorsAsBRLists(this ModelStateDictionary modelState)
		{

			var lis = modelState.GetErrorsAsFlatList().Select(e => HttpUtility.HtmlEncode(e.ErrorMessage.ToString())).ToList();

			return lis;
		}

	}
}