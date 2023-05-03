using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using TouchColombia.BusinessObjects.Entities;
using TouchColombia.BusinessObjects.Enums;
using TouchColombia.Controller;
using WebApplication2.Clases;
using System.Web.UI.WebControls;
using System.Collections.Concurrent;
using System.Text;
using System.Text.RegularExpressions;

namespace WebApplication2
{
    public partial class CrearUsuario : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string cargo = Session["CargoUsuario"].ToString();
            if (cargo != "ADMINISTRADOR")
            {
                Session["ERROR"] = "True";
                
                Response.Redirect("Alarmas.aspx");
            }
            if (!IsPostBack)
            {
                LlenarListadoUsuarios();
                llenarUsuario(); 
                cmbinserCargo.Items.Add("CONSULTA");
                cmbinserCargo.Items.Add("ANULACION");
                cmbinserCargo.Items.Add("ADMINISTRADOR");
                cmbactCargo.Items.Add("CONSULTA");
                cmbactCargo.Items.Add("ANULACION");
                cmbactCargo.Items.Add("ADMINISTRADOR");
                cmbactActivo.Items.Add("INACTIVO");
                cmbactActivo.Items.Add("ACTIVO");
            }
        }

        Controller oController = new Controller();

        private void LlenarListadoUsuarios()
        {
            try
            {
                if (oController.ObtenerUsuarios())
                {
                    GVUsuarios.DataSource = ObtenerTablasUsuario(oController.LstUsuario);
                    GVUsuarios.DataBind();
                }
            }
            catch (Exception e)
            { 
                
            }
        }

        private DataTable ObtenerTablasUsuario(List<Usuario> lstUsuario)
        {
            DataTable tUsuario = new DataTable();
            tUsuario.Columns.Add("Select");
            tUsuario.Columns.Add("ID");
            tUsuario.Columns.Add("Nombres");
            tUsuario.Columns.Add("Apellidos");
            tUsuario.Columns.Add("Contra");
            tUsuario.Columns.Add("Cargo");
            tUsuario.Columns.Add("Activo");
            tUsuario.Columns.Add("Fecha");
            tUsuario.Columns.Add("UsuarioCreador");

            foreach (Usuario oUsuario in lstUsuario)
            {
                DataRow fila = tUsuario.NewRow();
                fila["ID"] = oUsuario.Identificacion;
                fila["Nombres"] = oUsuario.Nombres;
                fila["Apellidos"] = oUsuario.Apellidos;
                fila["Contra"] = oUsuario.Contraseña;
                fila["Cargo"] = oUsuario.Cargo;
                fila["Activo"] = oUsuario.Activo;
                fila["Fecha"] = oUsuario.FechaCreacion;
                fila["UsuarioCreador"] = oUsuario.IdentUsuCreacion;
                tUsuario.Rows.Add(fila);
            }

            return tUsuario;
        }

        private void llenarUsuario()
        {
            if (!IsPostBack)
            {
                if (Session["NombreUsuario"] != null)
                {
                    string nombre = Session["NombreUsuario"].ToString();
                    string apellido = Session["ApellidoUsuario"].ToString();
                    string pass = Session["PassUsuario"].ToString();
                    txtPassC.Text = pass;
                    lblUser.Text = nombre + " " + apellido;
                }
                else
                {
                    Response.Redirect("Login.aspx");
                }
            }
        }

        protected void btnCambiar_Click(object sender, EventArgs e)
        {
            try
            {
                int id = Convert.ToInt32(Session["IdUsuario"]);
                string passA = txtPassA.Text;
                string passN = txtPassN.Text;
                string passC = txtPassC.Text;

                if (passA == Session["PassUsuario"].ToString())
                {
                    if (passN == passC)
                    {
                        Session["PassUsuario"] = passN;
                        if (oController.EditarContraseñaUsuario(id, passN))
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Contraseña cambiada con éxito');", true);
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Error al cambiar la contraseña');", true);
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Las contraseñas no coinciden');", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Contraseña Incorrecta');", true);
                }
            }
            catch (Exception ex) { }
        }

        protected void btnCerrar_Click(object sender, EventArgs e)
        {
            Session.RemoveAll();
            Response.Redirect("Login.aspx");
        }

        protected void GVUsuarios_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow oGridViewRow = GVUsuarios.SelectedRow;

            GVUsuarios.SelectedRowStyle.BackColor = Color.LightCyan;
            string sId = oGridViewRow.Cells[1].Text.ToString();
            string sNombres = oGridViewRow.Cells[2].Text.ToString();
            string sApellidos = oGridViewRow.Cells[3].Text.ToString();
            string sCargo = oGridViewRow.Cells[4].Text.ToString();
            string sActivo = oGridViewRow.Cells[5].Text.ToString();
            string sFecha = oGridViewRow.Cells[6].Text.ToString();

            Regex replace_a_Accents = new Regex("&#225;", RegexOptions.Compiled);
            Regex replace_e_Accents = new Regex("&#233;", RegexOptions.Compiled);
            Regex replace_i_Accents = new Regex("&#237;", RegexOptions.Compiled);
            Regex replace_o_Accents = new Regex("&#243;", RegexOptions.Compiled);
            Regex replace_u_Accents = new Regex("&#250;", RegexOptions.Compiled);
            Regex replace_n_Accents = new Regex("&#241;", RegexOptions.Compiled);
            Regex replace_space_Accents = new Regex("&#160;", RegexOptions.Compiled);

            sNombres = replace_a_Accents.Replace(sNombres, "á");
            sNombres = replace_e_Accents.Replace(sNombres, "é");
            sNombres = replace_i_Accents.Replace(sNombres, "í");
            sNombres = replace_o_Accents.Replace(sNombres, "ó");
            sNombres = replace_u_Accents.Replace(sNombres, "ú");
            sNombres = replace_n_Accents.Replace(sNombres, "ñ");
            sNombres = replace_space_Accents.Replace(sNombres, " ");

            sApellidos = replace_a_Accents.Replace(sApellidos, "á");
            sApellidos = replace_e_Accents.Replace(sApellidos, "é");
            sApellidos = replace_i_Accents.Replace(sApellidos, "í");
            sApellidos = replace_o_Accents.Replace(sApellidos, "ó");
            sApellidos = replace_u_Accents.Replace(sApellidos, "ú");
            sApellidos = replace_n_Accents.Replace(sApellidos, "ñ");
            sApellidos = replace_space_Accents.Replace(sApellidos, " ");

            txtactID.Text = sId;
            txtactNom.Text = sNombres;
            txtactApell.Text = sApellidos;

            switch (sCargo)
            {
                case "CONSULTA":

                    cmbactCargo.SelectedIndex = 0;

                break;

                case "ANULACION":

                cmbactCargo.SelectedIndex = 1;

                break;

                case "ADMINISTRADOR":

                cmbactCargo.SelectedIndex = 2;

                break;
            }

            switch (sActivo)
            {
                case "True":

                    cmbactActivo.SelectedIndex = 1;

                    break;

                case "False":

                    cmbactActivo.SelectedIndex = 0;

                    break;
            }
            
        }

        protected void btnIns_Click(object sender, EventArgs e)
        {
            long sId = Convert.ToInt32(txtinserID.Text);
            string sNombres = txtinserNom.Text;
            string sApellidos = txtinserApell.Text;
            string sContra = txtinserContra.Text;
            string sCargo = cmbinserCargo.Text;
            string sFecha = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");;
            long sUsu = Convert.ToInt32(Session["IdUsuario"]);

            if (oController.InsertarUsuario(sId, sContra, sNombres, sApellidos, sCargo, DateTime.Parse(sFecha), sUsu))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Usuario insertado');", true);
                LlenarListadoUsuarios();

                txtinserID.Text = string.Empty;
                txtinserNom.Text = string.Empty;
                txtinserApell.Text = string.Empty;
                txtinserContra.Text = string.Empty;
                cmbinserCargo.SelectedIndex = 0;
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Error al insertar el usuario');", true);
                LlenarListadoUsuarios();
            }
        }

        protected void btnAct_Click(object sender, EventArgs e)
        {
            long sId = Convert.ToInt32(txtactID.Text);
            string sNombres = txtactNom.Text;
            string sApellidos = txtactApell.Text;
            string sContra = txtactContra.Text;
            string sCargo = cmbactCargo.Text;
            bool sActivo = Convert.ToBoolean(cmbactActivo.SelectedIndex);

            if (oController.ActualizarUsuario(sId, sContra, sNombres, sApellidos, sCargo, sActivo))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Usuario actualizado');", true);
                LlenarListadoUsuarios();

                txtactID.Text = string.Empty;
                txtactNom.Text = string.Empty;
                txtactApell.Text = string.Empty;
                txtactContra.Text = string.Empty;
                cmbactCargo.SelectedIndex = 0;
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Error al actualizar el usuario');", true);
                LlenarListadoUsuarios();
            }
        }
    }
}