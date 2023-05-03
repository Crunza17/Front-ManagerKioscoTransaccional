using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TouchColombia.BusinessObjects;
using TouchColombia.BusinessObjects.Entities;
using TouchColombia.Controller;

namespace WebApplication2
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session.Remove("ERROR");
        }

        Controller oController = new Controller();

        protected void btnIngresar_Click(object sender, EventArgs e)
        {
            try 
            {
                long id = Convert.ToInt64(txtUser.Text);
                string contraseña = txtPass.Text;
                string nombre, apellido, cargo;
                bool activo;
                Session["IdUsuario"] = id;
                Session["PassUsuario"] = contraseña;

                if ((id != null) && (contraseña != string.Empty)) 
                {
                    if (oController.ObtenerUsuarioLogin(id, contraseña))
                    {
                        foreach (UsuarioLogin dat in oController.lstUsuarioLogin)
                        {
                            int i = 0;
                            nombre = dat.Nombres.ToString();
                            apellido = dat.Apellidos.ToString();
                            cargo = dat.Cargo.ToString();
                            activo = dat.Activo;
                            Session["NombreUsuario"] = nombre;
                            Session["ApellidoUsuario"] = apellido;
                            Session["CargoUsuario"] = cargo;

                            if (activo == true)
                            {
                                Response.Redirect("Alarmas.aspx");
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Usuario Inactivo');", true);
                            }
                            i++;
                        }
                        
                    }
                    else {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Identificación o Contraseña incorrectas');", true);
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Identificación o contraseña incorrectas');", true);
            }
        }
    }
}