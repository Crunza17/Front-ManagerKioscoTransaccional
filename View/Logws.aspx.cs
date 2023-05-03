using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TouchColombia.BusinessObjects.Entities;
using TouchColombia.BusinessObjects.Enums;
using TouchColombia.Controller;

namespace WebApplication2
{
    public partial class Logws : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            llenarCmb();
            RegistrarScript();
            llenarUsuario();

            Session.Remove("ERROR");
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
                    if (oController.ObtenerMetodos())
                    {
                        cmbMetodo.Items.Add("TODOS");
                        foreach (string sMetodo in oController.lstMetodo)
                        {
                            cmbMetodo.Items.Add(sMetodo);
                        }
                    }
                    if (Session["logcargado"] != null)
                    {
                        GVLogs.DataSource = Session["logcargado"];
                        GVLogs.DataBind();
                        cmbkio.SelectedIndex = Convert.ToInt16(Session["LkioscoID"]);
                        cmbMetodo.SelectedIndex = Convert.ToInt16(Session["LMetodoIn"]);
                        txtDoc.Text = Session["LDocumento"].ToString();
                        txtTran.Text = Session["LTransaccion"].ToString();
                        DateTime fechaini = Convert.ToDateTime(Session["LFechaI"].ToString());
                        DateTime fechafin = Convert.ToDateTime(Session["LFechaF"].ToString());
                        dtpInicio.Text = fechaini.ToString("yyyy-MM-dd");
                        dtpFin.Text = fechafin.ToString("yyyy-MM-dd");
                        
                    }
                    else if (Session["logcargado"] == null)
                    {
                        string fechaactual = DateTime.Now.ToString("yyyy-MM-dd");
                        dtpInicio.Text = fechaactual;
                        dtpFin.Text = fechaactual;
                    }
                }
            }
            catch (Exception e)
            {

            }
        }

        private DataTable ObtenerTablaLogs(List<LogWS> lstLogWS)
        {
            DataTable tLogs = new DataTable();
            tLogs.Columns.Add("IdLog");
            tLogs.Columns.Add("Fecha");
            tLogs.Columns.Add("IdCajero");
            tLogs.Columns.Add("NomSede");
            tLogs.Columns.Add("IdDocumento");
            tLogs.Columns.Add("IdTransaccion");
            tLogs.Columns.Add("Metodo");
            tLogs.Columns.Add("Entrada");
            tLogs.Columns.Add("Salida");

            foreach (LogWS oLogWS in lstLogWS)
            {
                DataRow fila = tLogs.NewRow();
                fila["IdLog"] = oLogWS.IdLog;
                fila["Fecha"] = oLogWS.FechaLog.ToString("yyyy/MM/dd hh:mm:ss");
                fila["IdCajero"] = oLogWS.IdCajero;
                fila["NomSede"] = oLogWS.Sede;
                fila["IdDocumento"] = oLogWS.IdDocumento;
                fila["IdTransaccion"] = oLogWS.IdTransaccion;
                fila["Metodo"] = oLogWS.Metodo;
                fila["Entrada"] = oLogWS.Entrada;
                fila["Salida"] = oLogWS.Salida;
                tLogs.Rows.Add(fila);
            }

            return tLogs;
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

        protected void btnConsultar_Click(object sender, EventArgs e)
        {
            try
            {
                string sKiosco = cmbkio.SelectedItem.ToString();
                string sSede = cmbsedes.SelectedItem.ToString();
                string sDocumento = txtDoc.Text;
                string sTransaccion = txtTran.Text;
                string sMetodo = cmbMetodo.Text;

                if (sKiosco == "TODOS") { sKiosco = null; }
                if (sSede == "TODOS") { sSede = null; }
                if (sDocumento == "") { sDocumento = null; }
                if (sMetodo == "TODOS") { sMetodo = null; }
                if (sTransaccion == "") { sTransaccion = "1234567890"; }

                if (sKiosco != string.Empty)
                {
                    if (sKiosco != null)
                    {
                        char delimitador = ' ';
                        string[] resultado = sKiosco.Split(delimitador);
                        sKiosco = resultado[0];
                    }
                    DateTime FechaIni = Convert.ToDateTime(dtpInicio.Text);
                    DateTime FechaFin = Convert.ToDateTime(dtpFin.Text);
                    if (oController.ObtenerLogws(sKiosco, sDocumento, Convert.ToInt32(sTransaccion), sMetodo, FechaIni, FechaFin, sSede))
                    {
                        GVLogs.DataSource = ObtenerTablaLogs(oController.lstLogsws);
                        GVLogs.DataBind();

                        DataTable sesion = ObtenerTablaLogs(oController.lstLogsws);
                        Session["logcargado"] = sesion;
                        Session["LKiosko"] = cmbkio.Text;
                        Session["LkioscoID"] = cmbkio.SelectedIndex;
                        Session["LDocumento"] = txtDoc.Text;
                        Session["LTransaccion"] = txtTran.Text;
                        Session["LMetodo"] = cmbMetodo.Text;
                        Session["LMetodoIn"] = cmbMetodo.SelectedIndex;
                        Session["LFechaI"] = dtpInicio.Text;
                        Session["LFechaF"] = dtpFin.Text;

                        ScriptManager.RegisterStartupScript(this, this.GetType(), "fnActivar", "fnActivar();", true);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Consulta exitosa');", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "fnActivar", "fnActivar();", true);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('No hay coincidencia');", true);
                        GVLogs.EmptyDataText = "No hay coincidencias";
                        GVLogs.BorderStyle = BorderStyle.None;
                        GVLogs.DataBind();
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "fnActivar", "fnActivar();", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Problemas con la consulta');", true);
                GVLogs.EmptyDataText = "Problemas con la consulta";
                GVLogs.BorderStyle = BorderStyle.None;
                GVLogs.DataBind();
            }
        }

        protected void btnExportar_Click(object sender, EventArgs e)
        {
            try
            {
                if (GVLogs.Rows.Count != 0)
                {
                    Response.Clear();
                    string fechaactual = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss tt");
                    Response.AddHeader("content-disposition", "attachment;filename = Logs-"+fechaactual+".xls");
                    Response.ContentType = "application/vnd.xls";

                    System.IO.StringWriter stringWriter = new System.IO.StringWriter();

                    System.Web.UI.HtmlTextWriter htmlTextWriter = new HtmlTextWriter(stringWriter);
                    GVLogs.RenderControl(htmlTextWriter);
                    Response.Write(stringWriter.ToString());

                    Response.End();
                }
                else if (GVLogs.Rows.Count == 0)
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