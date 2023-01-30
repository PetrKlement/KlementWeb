using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace KlementWeb.Classes
{
    public static class Extensions
    {
       
        public static IHtmlContent RenderFlashMessages(this IHtmlHelper helper)
        {
            var messageList = helper.ViewContext.TempData
                .DeserializeToObject<List<FlashMessage>>("Messages");

            var html = new HtmlContentBuilder();

            //foreach every messages and create HTML
            foreach (var msg in messageList)
            {
                var container = new TagBuilder("div");
                container.AddCssClass($"alert alert-{ msg.Type.ToString().ToLower() }"); //přidáme CSS z Bootstrap
                container.InnerHtml.SetContent(msg.Message);

                html.AppendHtml(container);
            }

            return html;
        }

        #region Controllers function
        /// <summary>
        /// Add FlashMessage to TempData in Json format
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="message"></param>

        public static void AddFlashMessage(this Controller controller, FlashMessage message)
        {
            var list = controller.TempData.DeserializeToObject<List<FlashMessage>>("Messages");
            list.Add(message);
            controller.TempData.SerializeObject(list, "Messages");  //save list to collection
        }
        /// <summary>
        /// Add FlashMessage to TempData in Json format
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="message"></param>
        /// <param name="messageType"></param>
        public static void AddFlashMessage(this Controller controller, string message, FlashMessageType messageType)
        {
            controller.AddFlashMessage(new FlashMessage(message, messageType));
        }

        public static void AddDebugMessage(this Controller controller, Exception ex)
        {
            string message = ex.Message;

            if (ex.GetBaseException().Message != message)
                message += Environment.NewLine + ex.GetBaseException().Message;

            AddFlashMessage(controller, new FlashMessage(message, FlashMessageType.Danger));
        }
        #endregion

        #region  Serialization/Deserialization
        /// <summary>
        /// Deserialize from JSON to object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="tempData"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static T DeserializeToObject<T>(this ITempDataDictionary tempData, string key) where T : new()
        {
            //looking anything in TempData and deserialize this
            string text = tempData[key]?.ToString();
            T result = text == null
                ? new T()
                : JsonConvert.DeserializeObject<T>(text);

            return result;
        }

        /// <summary>
        /// Serialize object to JSON
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="tempdata"></param>
        /// <param name="obj"></param>
        /// <param name="key"></param>
        public static void SerializeObject<T>(this ITempDataDictionary tempdata, T obj, string key)
        {
            tempdata[key] = JsonConvert.SerializeObject(obj, Formatting.Indented);
        }
        #endregion 
    }
}

