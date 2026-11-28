using System;
using Business_Logic_Layer;
using Business_Logic_Layer.Security;

namespace UI_Layer
{
    public partial class Register : System.Web.UI.Page
    {
        private readonly AuthService authService = new AuthService();

        protected void registerButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(username.Text) ||
                string.IsNullOrWhiteSpace(email.Text) ||
                string.IsNullOrWhiteSpace(password.Text))
            {
                Response.Write("All fields are required.");
                return;
            }

            if (password.Text != confirm_password.Text)
            {
                Response.Write("Passwords do not match.");
                return;
            }

            if (authService.UserExists(username.Text, email.Text))
            {
                Response.Write("Username or email already exists.");
                return;
            }

            string passwordHash = PasswordHasher.Hash(password.Text);
            int userId = authService.CreateUser(username.Text, email.Text, passwordHash);
            Session["UserId"] = userId;
            Session["Username"] = username.Text;

            Response.Redirect("~/ShopPage.aspx");
        }


        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}