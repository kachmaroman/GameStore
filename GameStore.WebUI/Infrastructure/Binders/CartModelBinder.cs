using GameStore.Domain.Entities;
using System.Web.Mvc;

namespace GameStore.WebUI.Infrastructure.Binders
{
    public class CartModelBinder : IModelBinder
    {
        private const string SessionKey = "Cart";

        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var cart = (Cart)controllerContext.HttpContext.Session[SessionKey];

            if (cart == null)
            {
                cart = new Cart();
                controllerContext.HttpContext.Session[SessionKey] = cart;
            }

            return cart;
        }
    }
}