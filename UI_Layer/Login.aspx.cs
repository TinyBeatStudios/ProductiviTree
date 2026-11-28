using System;
using Business_Logic_Layer;

namespace UI_Layer
{
    public partial class Login : System.Web.UI.Page
    {
        private readonly AuthService authService = new AuthService();

        protected void loginButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(username.Text) ||
                string.IsNullOrWhiteSpace(password.Text))
            {
                Response.Write("Username and password are required.");
                return;
            }

            AuthenticatedUser user = authService.AuthenticateUser(username.Text, password.Text);

            if (user == null)
            {
                Response.Write("Invalid username or password.");
                return;
            }

            Session["UserId"] = user.UserId;
            Session["Username"] = user.Username;
            Response.Redirect("~/ShopPage.aspx");
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}