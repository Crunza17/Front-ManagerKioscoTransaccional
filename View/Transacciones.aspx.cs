using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TouchColombia.BusinessObjects.Entities;
using TouchColombia.Controller;

namespace WebApplication2
{
    public partial class WebForm7 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            llenarCmb();
            RegistrarScript();
            llenarUsuario();

            Session.Remove("ERROR");
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

        private void llenarCmb()
        {
            try
            {
                if (!IsPostBack)
                {
                    cmbEspecialidad.Items.Add("TODOS");
                    cmbServicio.Items.Add("TODOS");
                    cmbkio.Items.Add("TODOS");

                    if (oController.ObtenerTipoServicio())
                    {
                        foreach (string sServicio in oController.lstServicios)
                        {
                            cmbServicio.Items.Add(sServicio);
                        }
                    }
                    if (oController.ObtenerEspecialidad())
                    {
                        foreach (string sEspecialidad in oController.lstEspecialidad)
                        {
                            cmbEspecialidad.Items.Add(sEspecialidad);
                        }
                    }
                    if (oController.ObtenerKioscos())
                    {
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

                    if (Session["transaccioncargada"] != null)
                    {
                        GVTransacciones.DataSource = Session["transaccioncargada"];
                        GVTransacciones.DataBind();

                        DateTime fechaini = Convert.ToDateTime(Session["TFechaIni"].ToString());
                        DateTime fechafin = Convert.ToDateTime(Session["TFechaFin"].ToString());

                        dtpFechaIni.Text = fechaini.ToString("yyyy-MM-dd");
                        dtpFechaF.Text = fechafin.ToString("yyyy-MM-dd");
                        cmbServicio.SelectedIndex = Convert.ToInt16(Session["TServicio"]);
                        cmbEspecialidad.SelectedIndex = Convert.ToInt16(Session["TEspecialidad"]);
                    }
                    else
                    {
                        string fechaactual = DateTime.Now.ToString("yyyy-MM-dd");
                        dtpFechaIni.Text = fechaactual;
                        dtpFechaF.Text = fechaactual;
                    }
                }
            }
            catch (Exception e)
            { 
                
            }
        }

        protected void btnConsultar_Click(object sender, EventArgs e)
        {
            ActualizarTransaccion();
        }

        private void ActualizarTransaccion()
        {
            try
            {
                string sKiosco = cmbkio.SelectedIndex >= 0 ? cmbkio.SelectedItem.ToString() : string.Empty;
                string sTipoSrv = cmbServicio.SelectedIndex >= 0 ? cmbServicio.SelectedItem.ToString() : string.Empty;
                string sEspecialidad = cmbEspecialidad.SelectedIndex >= 0 ? cmbEspecialidad.SelectedItem.ToString() : string.Empty;
                string sSede = cmbsedes.SelectedIndex >= 0 ? cmbsedes.SelectedItem.ToString() : string.Empty;

                char delimitador = ' ';
                string[] resultado = sKiosco.Split(delimitador);

                DateTime sFechaIni = DateTime.Parse(dtpFechaIni.Text);
                DateTime sFechaF = DateTime.Parse(dtpFechaF.Text);

                if ((sTipoSrv != string.Empty) && (sEspecialidad != string.Empty))
                {
                    if (oController.ObtenerTransacciones(resultado[0], sSede, sEspecialidad, sTipoSrv, sFechaIni, sFechaF))
                    {
                        GVTransacciones.DataSource = ObtenerTablaTransacciones(oController.lstTransaccion);
                        GVTransacciones.DataBind();

                        DataTable sesion = ObtenerTablaTransacciones(oController.lstTransaccion);

                        /* Se crean variables Session para que los datos se mantengan al cambiar de página */
                        Session["transaccioncargada"] = sesion;
                        Session["TServicio"] = cmbServicio.Text;
                        Session["TEspecialidad"] = cmbEspecialidad.Text;
                        Session["TFechaIni"] = sFechaIni;
                        Session["TFechaFin"] = sFechaF;
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "fnActivar", "fnActivar();", true);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Consulta exitosa');", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "fnActivar", "fnActivar();", true);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('No hay coincidencias');", true);
                        GVTransacciones.EmptyDataText = "No hay coincidencias";
                        GVTransacciones.DataBind();
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "fnActivar", "fnActivar();", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('No hay coincidencias');", true);

                GVTransacciones.EmptyDataText = "No hay coincidencias";
                GVTransacciones.DataBind();
            }
        }

        private void ActualizarTransaccionPorDoc()
        {
            try
            {
                string sDocumento = txtDoc.Text;

                if (sDocumento != string.Empty) 
                {
                    if (oController.ObtenerTransaccionesPorDocumento(sDocumento))
                    {
                        GVTransacciones.DataSource = ObtenerTablaTransacciones(oController.lstTransaccion);
                        GVTransacciones.DataBind();

                        DataTable sesion = ObtenerTablaTransacciones(oController.lstTransaccion);

                        /* Se crean variables Session para que los datos se mantengan al cambiar de página */
                        Session["transaccioncargada"] = sesion;
                        Session["TServicio"] = cmbServicio.Text;
                        Session["TEspecialidad"] = cmbEspecialidad.Text;
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "fnActivar", "fnActivar();", true);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Consulta exitosa');", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "fnActivar", "fnActivar();", true);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('No hay coincidencias');", true);
                        GVTransacciones.EmptyDataText = "No hay coincidencias";
                        GVTransacciones.DataBind();
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "fnActivar", "fnActivar();", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('No hay coincidencias');", true);

                GVTransacciones.EmptyDataText = "No hay coincidencias";
                GVTransacciones.DataBind();
            }
        }

        private DataTable ObtenerTablaTransacciones(List<Transaccion> lstTransaccion)
        {
            DataTable tTransaccion = new DataTable();
            tTransaccion.Columns.Add("Select");
            tTransaccion.Columns.Add("ID");
            tTransaccion.Columns.Add("Kiosco");
            tTransaccion.Columns.Add("NomSede");
            tTransaccion.Columns.Add("Servicio");
            tTransaccion.Columns.Add("TipoDoc");
            tTransaccion.Columns.Add("Documento");
            tTransaccion.Columns.Add("Usuario");
            tTransaccion.Columns.Add("Paciente");
            tTransaccion.Columns.Add("Especialidad");
            tTransaccion.Columns.Add("CentroMedico");
            tTransaccion.Columns.Add("Profesional");
            tTransaccion.Columns.Add("HoraCita");
            tTransaccion.Columns.Add("Observaciones");
            tTransaccion.Columns.Add("Estado");
            tTransaccion.Columns.Add("Fecha");

            foreach (Transaccion oTransaccion in lstTransaccion)
            {
                DataRow fila = tTransaccion.NewRow();
                fila["ID"] = oTransaccion.ID_Transaccion;
                fila["Kiosco"] = oTransaccion.ID_Modulo.ToString();
                fila["NomSede"] = oTransaccion.NomSede.ToString();
                fila["Servicio"] = oTransaccion.TipoServicio.ToString();
                fila["TipoDoc"] = oTransaccion.Tipo_Documento.ToString();
                fila["Documento"] = oTransaccion.NUM_Documento.ToString();
                fila["Usuario"] = oTransaccion.Usuario.ToString();
                fila["Paciente"] = oTransaccion.Nombre_Paciente.ToString();
                fila["Especialidad"] = oTransaccion.Especialidad.ToString();
                fila["CentroMedico"] = oTransaccion.CentroMedico.ToString();
                fila["Profesional"] = oTransaccion.Profesional;
                fila["HoraCita"] = oTransaccion.FechaHoraCita;
                fila["Observaciones"] = oTransaccion.Observaciones;
                fila["Estado"] = oTransaccion.Estado;
                string Estado = oTransaccion.Estado.ToString();
                switch (Estado)
                {
                    case "1":
                        fila["Estado"] = "Aprobado";
                        break;
                    case "2":
                        fila["Estado"] = "Cancelado";
                        break;
                    case "3":
                        fila["Estado"] = "N/A";
                        break;
                    case "4":
                        fila["Estado"] = "Error Billetero";
                        break;
                    case "5":
                        fila["Estado"] = "Error WebService";
                        break;
                    case "6":
                        fila["Estado"] = "Error Impresora";
                        break;
                }
                fila["Fecha"] = oTransaccion.Fecha;
                tTransaccion.Rows.Add(fila);
            }

            return tTransaccion;
        }

        protected void btnExportar_Click(object sender, EventArgs e)
        {
            try
            {
                if (GVTransacciones.Rows.Count != 0)
                {
                    Response.Clear();
                    string fechaactual = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss tt");
                    Response.AddHeader("content-disposition", "attachment;filename = Transacciones-"+fechaactual+".xls");
                    Response.ContentType = "application/vnd.xls";

                    System.IO.StringWriter stringWriter = new System.IO.StringWriter();

                    System.Web.UI.HtmlTextWriter htmlTextWriter = new HtmlTextWriter(stringWriter);
                    GVTransacciones.RenderControl(htmlTextWriter);
                    Response.Write(stringWriter.ToString());

                    Response.End();
                }
                else if (GVTransacciones.Rows.Count == 0)
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

        private void RegistrarScript()
        {
            const string ScriptKey = "ScriptKey";
            if (!ClientScript.IsStartupScriptRegistered(this.GetType(), ScriptKey))
            {
                StringBuilder fn = new StringBuilder();
                fn.Append("function fnActivar() {");
                fn.Append("document.getElementById(\"btnConsultar\").style.pointerEvents = 'auto'; ");
                fn.Append("document.getElementById(\"btnBuscar\").style.pointerEvents = 'auto'; ");
                fn.Append("document.getElementById(\"ImgLoad\").style.display = 'none';");
                fn.Append("}");
                ClientScript.RegisterStartupScript(this.GetType(),
                ScriptKey, fn.ToString(), true);
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

        protected void Button2_Click(object sender, EventArgs e)
        {
            ActualizarTransaccionPorDoc();
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