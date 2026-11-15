using System;
using System.Collections.Generic;
using System.Linq;
using Business_Logic_Layer;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace UI_Layer
{
    public partial class ShopPage : System.Web.UI.Page
    {
        private readonly ShopService _shopService = new ShopService();

        private int CurrentUserId
        {
            get
            {
                if (Session["UserId"] == null)
                {
                    Session["UserId"] = 1; // Default user ID for testing
                }
                return (int)Session["UserId"];
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindShop();
            }
        }

        protected void ShopRepeater_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            int itemId = int.Parse((string)e.CommandArgument);

            if (e.CommandName == "Buy")
            {
                switch (_shopService.Purchase(CurrentUserId, itemId))
                {
                    case PurchaseResult.Success:
                        MessageLabel.Text = "Purchase successful! Don't forget to equip it!";
                        break;
                    case PurchaseResult.NotEnoughMoney:
                        MessageLabel.Text = "Not Enough coins!";
                        break;
                    case PurchaseResult.AlreadyOwned:
                        MessageLabel.Text = "You already own this item!";
                        break;
                    default:
                        MessageLabel.Text = "Item not found!";
                        break;
                }
            }
            else if (e.CommandName == "Equip")
            {
                MessageLabel.Text = _shopService.Equip(CurrentUserId, itemId) ? "Item equipped!" : "You can't Equip That!";
            }

            BindShop();
        }

        private void BindShop()
        {
            BalanceLabel.Text = _shopService.GetCurrency(CurrentUserId).ToString();
            ShopRepeater.DataSource = _shopService.GetShopItems(CurrentUserId);
            ShopRepeater.DataBind();
        }



    }
}