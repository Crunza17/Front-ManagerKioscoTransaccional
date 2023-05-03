using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TouchColombia.BusinessObjects.Entities;
using TouchColombia.Controller;

namespace WebApplication2
{
    public partial class WebForm6 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            llenarCmb();
            llenarUsuario();
            RegistrarScript();

            Session.Remove("ERROR");
        }

        protected void cmbkio_SelectedIndexChanged(object sender, EventArgs e)
        {
            
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

        Controller oController = new Controller();

        private void llenarCmb()
        {
            try
            {
                if (!IsPostBack)
                {
                    if (oController.ObtenerKioscos())
                    {
                        cmbkio.Items.Add("TODOS");
                        foreach (string sKiosco in oController.lstKioscos)
                        {
                            cmbkio.Items.Add(sKiosco);
                        }
                    }

                    if (oController.ObtenerNomSedes())
                    {
                        cmbsedes.Items.Add("TODOS");
                        foreach (string sSedes in oController.LstNomSedes)
                        {
                            cmbsedes.Items.Add(sSedes);
                        }
                    }

                    if (Session["AlarmaDetalle"] != null)
                    {
                        DateTime? fechaIn = null;
                        DateTime? fechaFn = null;
                        string sSede = null;
                        string sKiosco = Session["AlarmaDetalle"].ToString();
                        if (oController.ObtenerDetalleAlarmas(sKiosco, fechaIn, fechaFn, sSede))
                        {
                            GVDAlarma.DataSource = ObtenerTablaDetalleAlarmas(oController.lstDetalleAlarma);
                            GVDAlarma.DataBind();

                            DataTable sesion = ObtenerTablaDetalleAlarmas(oController.lstDetalleAlarma);

                            /* Se crean variables Session para que los datos se mantengan al cambiar de página */
                            Session["detallealarmacargado"] = sesion;
                            string fechaactual = DateTime.Now.ToString("yyyy-MM-dd");
                            dtpFechaIni.Text = fechaactual;
                            dtpFechaF.Text = fechaactual;
                            Session.Remove(Session["AlarmaDetalle"].ToString());
                        }
                        else
                        {
                            GVDAlarma.EmptyDataText = "No hay coincidencias";
                            GVDAlarma.DataBind();
                        }
                    }
                    else if (Session["detallealarmacargado"] != null)
                    {
                        GVDAlarma.DataSource = Session["detallealarmacargado"];
                        GVDAlarma.DataBind();
                        cmbkio.SelectedIndex = Convert.ToInt16(Session["DAKiosco"]);
                        DateTime fechaini = Convert.ToDateTime(Session["DAFechaIni"].ToString());
                        DateTime fechafin = Convert.ToDateTime(Session["DAFechaFin"].ToString());
                        dtpFechaIni.Text = fechaini.ToString("yyyy-MM-dd");
                        dtpFechaF.Text = fechafin.ToString("yyyy-MM-dd");
                    }
                    else if (Session["detallealarmacargado"] == null)
                    {
                        string fechaactual = DateTime.Now.ToString("yyyy-MM-dd");
                        dtpFechaIni.Text = fechaactual;
                        dtpFechaF.Text = fechaactual;
                    }
                }
            }
            catch (Exception e) 
            { }
        }

        private void CargarDetalle()
        {
            try
            {
                string sKiosco = cmbkio.SelectedIndex >= 0 ? cmbkio.SelectedItem.ToString() : string.Empty;
                string sSede = cmbsedes.SelectedItem.ToString();

                if (sSede == "TODOS") { sSede = null; }

                if (sKiosco != null)
                {
                    char delimitador = ' ';
                    string[] resultado = sKiosco.Split(delimitador);
                    sKiosco = resultado[0];
                } 

                if (sKiosco != string.Empty)
                {
                    DateTime dFechaIni = Convert.ToDateTime(dtpFechaIni.Text);
                    DateTime dFechaFin = Convert.ToDateTime(dtpFechaF.Text);
                    if (oController.ObtenerDetalleAlarmas(sKiosco, dFechaIni, dFechaFin, sSede))
                    {
                        GVDAlarma.DataSource = ObtenerTablaDetalleAlarmas(oController.lstDetalleAlarma);
                        GVDAlarma.DataBind();

                        DataTable sesion = ObtenerTablaDetalleAlarmas(oController.lstDetalleAlarma);

                        /* Se crean variables Session para que los datos se mantengan al cambiar de página */
                        Session["detallealarmacargado"] = sesion;
                        Session["DAKiosco"] = cmbkio.SelectedIndex;
                        Session["DAFechaIni"] = dtpFechaIni.Text;
                        Session["DAFechaFin"] = dtpFechaF.Text;
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "fnActivar", "fnActivar();", true);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Consulta exitosa');", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "fnActivar", "fnActivar();", true);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('No hay coincidencias');", true);
                        GVDAlarma.EmptyDataText = "No hay coincidencias";
                        GVDAlarma.BorderStyle = BorderStyle.None;
                        GVDAlarma.DataBind();
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "fnActivar", "fnActivar();", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Problemas con la consulta');", true);
                GVDAlarma.EmptyDataText = "Problemas con la consulta";
                GVDAlarma.BorderStyle = BorderStyle.None;
                GVDAlarma.DataBind();
            }
        }

        private DataTable ObtenerTablaDetalleAlarmas(List<DetalleAlarma> lstDeAlarmas)
        {
            DataTable tDAlarmas = new DataTable();
            tDAlarmas.Columns.Add("Alarma");
            tDAlarmas.Columns.Add("Cajero");
            tDAlarmas.Columns.Add("NomSede");
            tDAlarmas.Columns.Add("Error");
            tDAlarmas.Columns.Add("Prioridad");
            tDAlarmas.Columns.Add("Observacion");
            tDAlarmas.Columns.Add("Descripcion");
            tDAlarmas.Columns.Add("Fecha");

            foreach (DetalleAlarma oDeAlarmas in lstDeAlarmas)
            {
                DataRow fila = tDAlarmas.NewRow();
                fila["Alarma"] = oDeAlarmas.ID_Alarma;
                fila["Cajero"] = oDeAlarmas.ID_Pantalla;
                fila["NomSede"] = oDeAlarmas.Sede;
                fila["Error"] = oDeAlarmas.TipoError.ToString();
                fila["Prioridad"] = oDeAlarmas.PrioridadAlarma.ToString();
                fila["Observacion"] = oDeAlarmas.Observacion.ToString();
                fila["Descripcion"] = oDeAlarmas.DescripcionAlarma.ToString();
                fila["Fecha"] = oDeAlarmas.FechaA.ToString();
                tDAlarmas.Rows.Add(fila);
            }

            return tDAlarmas;
        }

        protected void btnExportar_Click(object sender, EventArgs e)
        {
            try
            {
                if (GVDAlarma.Rows.Count != 0)
                {
                    Response.Clear();
                    string fechaactual = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss tt");
                    Response.AddHeader("content-disposition", "attachment;filename = DetalleAlarma-" + fechaactual + ".xls");
                    Response.ContentType = "application/vnd.xls";

                    System.IO.StringWriter stringWriter = new System.IO.StringWriter();

                    System.Web.UI.HtmlTextWriter htmlTextWriter = new HtmlTextWriter(stringWriter);
                    GVDAlarma.RenderControl(htmlTextWriter);
                    Response.Write(stringWriter.ToString());

                    Response.End();
                }
                else if (GVDAlarma.Rows.Count == 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('La consulta está vacía');", true);
                }
            }
            catch (Exception ex)
            { }
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
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

        protected void btnConsultar_Click(object sender, EventArgs e)
        {
            CargarDetalle();
        }

        private void RegistrarScript()
        {
            const string ScriptKey = "ScriptKey";
            if (!ClientScript.IsStartupScriptRegistered(this.GetType(), ScriptKey))
            {
                StringBuilder fn = new StringBuilder();
                fn.Append("function fnActivar() {");
                fn.Append("document.getElementById(\"btnConsultar\").style.pointerEvents = 'auto'; ");
                fn.Append("document.getElementById(\"ImgLoad\").style.display = 'none';");
                fn.Append("}");
                ClientScript.RegisterStartupScript(this.GetType(),
                ScriptKey, fn.ToString(), true);
            }
        }

        protected void cmbsedes_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbkio.Items.Clear();
            string sSede = cmbsedes.SelectedItem.ToString();
            if (oController.ObtenerKioscosPorSede(sSede))
            {
                cmbkio.Items.Add("TODOS");
                foreach (string sKiosco in oController.lstKioscos)
                {
                    cmbkio.Items.Add(sKiosco);
                }
            }
        }
    }
}