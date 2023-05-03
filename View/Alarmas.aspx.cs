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


namespace WebApplication2
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e) //Se ejecuta al cargar la página
        {
            LlenarAlarmasEnLinea();
            llenarUsuario();
            LlenarMapa();
            if (Session["ERROR"] != null)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('No está autorizado');", true);
                Session.Remove("ERROR");
            }
            
        }


        Controller oController = new Controller();
		  
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

        private void LlenarAlarmasEnLinea() {

            try
            {
                if (oController.ObtenerAlarmasEnLinea())
                {
                    GVAlarmas.DataSource = ObtenerTablaAlarmasLínea(oController.lstAlarmas);
                    GVAlarmas.DataBind(); // Aquí se llena la tabla y se muestra
                    int i = 0;
                    foreach (GridViewRow row in GVAlarmas.Rows) //Recorre cada celda, y la que esté con un ID distinto a 0, le pone un color respectivo
                    {
                        if (oController.lstAlarmas[i].PrinterAlarm == 1)
                        {
                            row.Cells[2].BackColor = Color.Yellow;
                        }
                        else if (oController.lstAlarmas[i].PrinterAlarm == 2)
                        {
                            row.Cells[2].BackColor = Color.FromArgb(226, 0, 26);
                        }

                        if (oController.lstAlarmas[i].CardDeviceAlarm == 1)
                        {
                            row.Cells[3].BackColor = Color.Yellow;
                        }
                        else if (oController.lstAlarmas[i].CardDeviceAlarm == 2)
                        {
                            row.Cells[3].BackColor = Color.FromArgb(226, 0, 26);
                        }

                        if (oController.lstAlarmas[i].WebserviceAlarm == 1)
                        {
                            row.Cells[4].BackColor = Color.Yellow;
                        }
                        else if (oController.lstAlarmas[i].WebserviceAlarm == 2)
                        {
                            row.Cells[4].BackColor = Color.FromArgb(226, 0, 26);
                        }

                        if (oController.lstAlarmas[i].EnLinea == 0)
                        {
                            row.Cells[5].BackColor = Color.FromArgb(0, 226, 23);
                        }
                        else if (oController.lstAlarmas[i].EnLinea == 1)
                        {
                            row.Cells[5].BackColor = Color.Yellow;
                        }
                        else if (oController.lstAlarmas[i].EnLinea == 2)
                        {
                            row.Cells[5].BackColor = Color.FromArgb(226, 0, 26);
                        }
                        i++;
                    }
                }
            }
            catch (Exception e) 
            { }
        }

        private void LlenarMapa()
        {

            try
            {
                if (oController.ObtenerUbicaciones())
                {
                    int i = 0;
                    foreach (ubicaciones oUbicaciones in oController.LstUbicaciones)
                    {
                        string kiosko = oUbicaciones.Kiosko.ToString();
                        string direccion = oUbicaciones.Direccion.ToString();
                        int EnLinea = oUbicaciones.EnLinea;

                        string script = string.Format("obtenerCoordenadas('{0}','{1}','{2}');", direccion, kiosko, EnLinea);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "obtenerCoordenadas" + i, script, true);

                        i++;
                    }
                }
            }
            catch (Exception e)
            { }
        }


        private DataTable ObtenerTablaAlarmasLínea(List<Alarma> lstAlarmas)
        {
            DataTable tAlarmas = new DataTable();
            tAlarmas.Columns.Add("Select");
            tAlarmas.Columns.Add("IdCajero", typeof(string));
            tAlarmas.Columns.Add("Impresora", typeof(string));
            tAlarmas.Columns.Add("BarcodeReader", typeof(string));
            tAlarmas.Columns.Add("WS", typeof(string));
            tAlarmas.Columns.Add("EnLinea", typeof(string));
            tAlarmas.Columns.Add("Inactivo", typeof(string));

            foreach (Alarma oAlarma in lstAlarmas)
            {
                DataRow fila = tAlarmas.NewRow();
                fila["IdCajero"] = oAlarma.oModulo.ID_Modulo;
                fila["Impresora"] = "Printer";
                fila["BarcodeReader"] = "Card_Device";
                fila["WS"] = "Web_Service";
                fila["EnLinea"] = "EnLinea";
                fila["Inactivo"] = oAlarma.TiempoInactivo;
                tAlarmas.Rows.Add(fila);
            }

            return tAlarmas;
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
            catch (Exception ex) {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Error con el cambio de contraseña');", true);
            }
        }

        protected void btnCerrar_Click(object sender, EventArgs e)
        {
            Session.RemoveAll();
            Response.Redirect("Login.aspx");
        }

        protected void GVAlarmas_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow oGridViewRow = GVAlarmas.SelectedRow;
            string alarma = oGridViewRow.Cells[1].Text.ToString();
            string[] resultado = alarma.Split(' ');
            Session["AlarmaDetalle"] = resultado[0];

            Response.Redirect("DetalleAlarma.aspx");
        }
    }
}