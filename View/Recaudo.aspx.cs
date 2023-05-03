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
    public partial class WebForm4 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            llenarCmb();
            RegistrarScript();
            llenarUsuario();
            Session.Remove("ERROR");

            btnAnular.BackColor = Color.LightGray;
            btnAnular.ForeColor = Color.Gray;
            btnAnular.Attributes.Add("disabled", "disabled");

            btnSta.BackColor = Color.LightGray;
            btnSta.ForeColor = Color.Gray;
            btnSta.Attributes.Add("disabled", "disabled");
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
                    if (oController.ObtenerKioscos())
                    {
                        cmbkio.Items.Add("TODOS");
                        foreach (string sKiosco in oController.lstKioscos)
                        {
                            cmbkio.Items.Add(sKiosco);
                        }
                    }

                    if (oController.ObtenerEstados())
                    {
                        cmbestado.Items.Add("TODOS");
                        foreach (string sEstados in oController.lstEstado)
                        {
                            cmbestado.Items.Add(sEstados);
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

                    if (Session["recaudocargado"] != null)
                    {
                        GVRecaudo.DataSource = Session["recaudocargado"];
                        GVRecaudo.DataBind();

                        DateTime fechaini = Convert.ToDateTime(Session["RFechaIni"].ToString());
                        DateTime fechafin = Convert.ToDateTime(Session["RFechaFin"].ToString());
                        dtpFechaIni.Text = fechaini.ToString("yyyy-MM-dd");
                        dtpFechaF.Text = fechafin.ToString("yyyy-MM-dd");
                        cmbestado.SelectedIndex = Convert.ToInt16(Session["REstado"]);
                    }
                    else if (Session["recaudocargado"] == null)
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
        protected void btnExportar_Click(object sender, EventArgs e)
        {
            try
            {
                if (GVRecaudo.Rows.Count != 0)
                {
                    Response.Clear();
                    string fechaactual = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss tt");
                    Response.AddHeader("content-disposition", "attachment;filename = Recaudo-"+fechaactual+".xls");
                    Response.ContentType = "application/vnd.xls";

                    System.IO.StringWriter stringWriter = new System.IO.StringWriter();

                    System.Web.UI.HtmlTextWriter htmlTextWriter = new HtmlTextWriter(stringWriter);
                    GVRecaudo.RenderControl(htmlTextWriter);
                    Response.Write(stringWriter.ToString());

                    Response.End();
                }
                else if (GVRecaudo.Rows.Count == 0)
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

        private void LlenarRecaudo()
        {
            try
            {
                string sEstado = cmbestado.SelectedItem.Text;

                DateTime sFechaIni = DateTime.Parse(dtpFechaIni.Text);
                DateTime sFechaF = DateTime.Parse(dtpFechaF.Text);

                int cancelado = 0;
                int nocancelado = 0;

                GVRecaudo.Columns[10].ItemStyle.Width = 150;

                if (ckbCancelado.Checked == true) 
                {
                    cancelado = 1;
                }

                if (cknNoCancelado.Checked == true)
                {
                    nocancelado = 1;
                }

                if (sEstado != null)
                {
                    string sKiosco = cmbkio.SelectedIndex >= 0 ? cmbkio.SelectedItem.ToString() : string.Empty;
                    string sSede = cmbsedes.SelectedIndex >= 0 ? cmbsedes.SelectedItem.ToString() : string.Empty;
                    char delimitador = ' ';
                    string[] resultado = sKiosco.Split(delimitador);

                    if (oController.ObtenerRecaudo(resultado[0], sEstado, sFechaIni, sFechaF, cancelado, nocancelado, sSede))
                    {
                        GVRecaudo.DataSource = ObtenerTablaRecaudo(oController.LstRecaudo);
                        GVRecaudo.DataBind();

                        DataTable sesion = ObtenerTablaRecaudo(oController.LstRecaudo);

                        /* Se crean variables Session para que los datos se mantengan al cambiar de página */
                        Session["recaudocargado"] = sesion;
                        Session["REstado"] = cmbestado.SelectedIndex;
                        Session["RFechaIni"] = sFechaIni;
                        Session["RFechaFin"] = sFechaF;
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "fnActivar", "fnActivar();", true);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Consulta exitosa');", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "fnActivar", "fnActivar();", true);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('No hay coincidencias');", true);
                        
                        GVRecaudo.EmptyDataText = "No hay coincidencias";
                        GVRecaudo.DataBind();
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "fnActivar", "fnActivar();", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('No hay coincidencias');", true);

                GVRecaudo.EmptyDataText = "No hay coincidencias";
                GVRecaudo.DataBind();
            }
        }

        private void LlenarRecaudoPorDocumento()
        {
            try
            {
                string sDocumento = txtDoc.Text;

                if (sDocumento != string.Empty)
                {

                    if (oController.ObtenerRecaudoPorDocumento(sDocumento))
                    {
                        GVRecaudo.DataSource = ObtenerTablaRecaudo(oController.LstRecaudo);
                        GVRecaudo.DataBind();

                        DataTable sesion = ObtenerTablaRecaudo(oController.LstRecaudo);

                        /* Se crean variables Session para que los datos se mantengan al cambiar de página */
                        Session["recaudocargado"] = sesion;
                        Session["RDocumento"] = sDocumento;
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "fnActivar", "fnActivar();", true);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Consulta exitosa');", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "fnActivar", "fnActivar();", true);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('No hay coincidencias');", true);

                        GVRecaudo.EmptyDataText = "No hay coincidencias";
                        GVRecaudo.BorderStyle = BorderStyle.None;
                        GVRecaudo.DataBind();
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "fnActivar", "fnActivar();", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Problemas con la consulta');", true);

                GVRecaudo.EmptyDataText = "Problemas con la consulta";
                GVRecaudo.BorderStyle = BorderStyle.None;
                GVRecaudo.DataBind();
            }
        }



        private DataTable ObtenerTablaRecaudo(List<Pago> lstPago)
        {
            DataTable tPago = new DataTable();
            tPago.Columns.Add("Select");
            tPago.Columns.Add("ID");
            tPago.Columns.Add("Cajero");
            tPago.Columns.Add("NomSede");
            tPago.Columns.Add("Cita");
            tPago.Columns.Add("Modo");
            tPago.Columns.Add("Referencia");
            tPago.Columns.Add("Documento");
            tPago.Columns.Add("TipoDoc");
            tPago.Columns.Add("Nombres");
            tPago.Columns.Add("Apellidos");
            tPago.Columns.Add("Valor");
            tPago.Columns.Add("Factura");
            tPago.Columns.Add("FechaHora");
            tPago.Columns.Add("FechaPago");
            tPago.Columns.Add("ValorRecibido");
            tPago.Columns.Add("Estado");
            tPago.Columns.Add("Autorizacion");
            tPago.Columns.Add("TipoCuenta");
            tPago.Columns.Add("Franquicia");
            tPago.Columns.Add("Tarjeta");
            tPago.Columns.Add("Status");
            tPago.Columns.Add("Anulacion");
            tPago.Columns.Add("FechaAnu");
            tPago.Columns.Add("Usuario");
            tPago.Columns.Add("Observacion");

            foreach (Pago oPago in lstPago)
            {
                DataRow fila = tPago.NewRow();
                fila["ID"] = oPago.ID_TipoPago;
                fila["Cajero"] = oPago.ID_Cajero.ToString();
                fila["NomSede"] = oPago.Sede.ToString();
                fila["Cita"] = oPago.ID_Cita.ToString();
                fila["Modo"] = oPago.ModoPago.ToString();
                fila["Referencia"] = oPago.Referencia.ToString();
                fila["Documento"] = oPago.Documento.ToString();
                fila["TipoDoc"] = oPago.TipoDoc.ToString();
                switch (oPago.TipoDoc.ToString())
                {
                    case "1":
                        fila["TipoDoc"] = "CC";
                        break;
                    case "2":
                        fila["TipoDoc"] = "CE";
                        break;
                    case "3":
                        fila["TipoDoc"] = "TI";
                        break;
                    case "4":
                        fila["TipoDoc"] = "RC";
                        break;
                    case "5":
                        fila["TipoDoc"] = "PA";
                        break;
                    case "6":
                        fila["TipoDoc"] = "PE";
                        break;
                    case "7":
                        fila["TipoDoc"] = "PPT";
                        break;
                    default:
                        fila["TipoDoc"] = "Inválido";
                        break;
                }
                fila["Nombres"] = oPago.Nombres.ToString();
                fila["Apellidos"] = oPago.Apellidos.ToString();
                fila["Valor"] = oPago.Valor.ToString();
                fila["Factura"] = oPago.Factura.ToString();
                fila["FechaHora"] = oPago.FechaPago.ToString();
                fila["FechaPago"] = oPago.FechaPago.ToString("dd/MM/yyyy");
                fila["ValorRecibido"] = oPago.ValorRecibido.ToString();
                fila["Estado"] = oPago.Estado;
                string Estado = oPago.Estado.ToString();
                switch (Estado)
                {
                    case "1":
                        fila["Estado"] = "Aprobado";
                        break;
                    case "2":
                        fila["Estado"] = "Cancelado";
                        break;
                    case "5":
                        fila["Estado"] = "Error WebService";
                        break;
                    case "7":
                        fila["Estado"] = "Error Datafono";
                        break;
                }
                fila["Autorizacion"] = oPago.Autorizacion;
                fila["TipoCuenta"] = oPago.TipoCuenta;
                fila["Franquicia"] = oPago.Franquicia;
                fila["Tarjeta"] = oPago.Tarjeta;
                string Status = oPago.Referencia;
                switch (Status)
                {
                    case "":
                        fila["Status"] = "No Aplica";
                        break;
                    case "Pendiente":
                        fila["Status"] = "Cambio de Status";
                        break;
                    default:
                        fila["Status"] = oPago.Referencia;
                        break;
                }
                string Anulado = oPago.Anulacion.ToString();
                switch (Anulado)
                {
                    case "True":
                        fila["Anulacion"] = "Anulado";
                        break;
                    case "False":
                        fila["Anulacion"] = "No Aplica";
                        break;
                    default:
                        fila["Anulacion"] = "No Aplica";
                        break;
                }
                string FechaA = oPago.FechaAnu.ToString();
                switch (FechaA)
                {
                    case "1/01/1900 12:00:00 a. m.":
                        fila["FechaAnu"] = "No aplica";
                        break;
                    case "1/01/1900 12:00:00 p. m.":
                        fila["FechaAnu"] = "No aplica";
                        break;
                    default:
                        fila["FechaAnu"] = oPago.FechaAnu.ToString();
                        break;
                }
                fila["Usuario"] = oPago.Usuario.ToString();
                fila["Observacion"] = oPago.Observaciones.ToString();
                tPago.Rows.Add(fila);
            }

            return tPago;
        }

        protected void GVRecaudo_SelectedIndexChanged(object sender, EventArgs e)
        {
            string cargo = Session["CargoUsuario"].ToString();
            if (cargo != "CONSULTA")
            {
                GridViewRow oGridViewRow = GVRecaudo.SelectedRow;

                string sId = oGridViewRow.Cells[1].Text.ToString();
                string sStatus = oGridViewRow.Cells[18].Text.ToString();
                string sAnulado = oGridViewRow.Cells[19].Text.ToString();
                string sValor = oGridViewRow.Cells[10].Text.ToString();
                lblInfo.Text = "¿Está seguro de cancelar la transacción Identificada como el ID:" + sId + ", y con un valor de $" + sValor + "?";
                lblInfoS.Text = "¿Está seguro de cambiar el status de la transacción Identificada como el ID:" + sId + ", y con un valor de $" + sValor + "?";
                txtObservacionesStatus.Text = " ";
                TextArea.Text = " ";
                GVRecaudo.SelectedRowStyle.BackColor = Color.LightCyan;

                if ((sAnulado == "No Aplica") && (sStatus == "No Aplica"))
                {
                    btnAnular.BackColor = Color.Green;
                    btnAnular.ForeColor = Color.White;
                    Session["IdRecaudo"] = sId;
                    btnAnular.Attributes.Remove("disabled");

                    btnSta.BackColor = Color.Green;
                    btnSta.ForeColor = Color.White;
                    Session["IdRecaudo"] = sId;
                    btnSta.Attributes.Remove("disabled");
                }
                else if (sAnulado == "Anulado")
                {
                    btnSta.BackColor = Color.LightGray;
                    btnSta.ForeColor = Color.Gray;
                    btnSta.Attributes.Add("disabled", "disabled");

                    btnAnular.BackColor = Color.LightGray;
                    btnAnular.ForeColor = Color.Gray;
                    btnAnular.Attributes.Add("disabled", "disabled");
                }
                else if (sStatus == "Cambio de Status")
                {
                    btnSta.BackColor = Color.LightGray;
                    btnSta.ForeColor = Color.Gray;
                    btnSta.Attributes.Add("disabled", "disabled");

                    btnAnular.BackColor = Color.LightGray;
                    btnAnular.ForeColor = Color.Gray;
                    btnAnular.Attributes.Add("disabled", "disabled");
                }
            }
            else if (cargo == "CONSULTA")
            {
                btnAnular.BackColor = Color.LightGray;
                btnAnular.ForeColor = Color.Gray;
                btnAnular.Attributes.Add("disabled", "disabled");

                btnSta.BackColor = Color.LightGray;
                btnSta.ForeColor = Color.Gray;
                btnSta.Attributes.Add("disabled", "disabled");
            }
            
        }

        protected void btnAnularOB_Click(object sender, EventArgs e)
        {
            try
            {
                int id = Convert.ToInt32(Session["IdRecaudo"]);
                string usuario = Session["IdUsuario"].ToString();
                string fechaactual = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
                string observacion = TextArea.Text;
                observacion = observacion.Trim();

                if (observacion == "")
                    observacion = "NULO";


                if (usuario != null)
                {
                    if (observacion == "NULO")
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Observaciones vacías');", true);
                    }
                    else
                    {
                        if (oController.AnularPago(id, usuario, DateTime.Parse(fechaactual), observacion))
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Transacción anulada');", true);
                            LlenarRecaudo();
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Error al anular la transacción');", true);
                        }
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Vuelva a iniciar sesión);", true);
                }
            }
            catch (Exception ex) 
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Problemas con la anulación');", true);   
            }
        }

        protected void btnStatus_Click(object sender, EventArgs e)
        {
            try
            {
                int id = Convert.ToInt32(Session["IdRecaudo"]);
                string usuario = Session["IdUsuario"].ToString();
                string fechaactual = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
                string observacion = txtObservacionesStatus.Text;
                observacion = observacion.Trim();

                if (observacion == "")
                    observacion = "NULO";


                if (usuario != null)
                {
                    if (observacion == "NULO")
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Observaciones vacías');", true);
                    }
                    else
                    {
                        if (oController.CambioStatus(id, usuario, DateTime.Parse(fechaactual), observacion))
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Cambio de Status realizado');", true);
                            LlenarRecaudo();
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Error al realizar el cambio de status);", true);
                        }
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Vuelva a iniciar sesión');", true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Problemas con el cambio de status'');", true);
            }
        }

        protected void btnConsultar_Click(object sender, EventArgs e)
        {
            LlenarRecaudo();
            btnAnular.BackColor = Color.LightGray;
            btnAnular.ForeColor = Color.Gray;
            btnAnular.Attributes.Add("disabled", "disabled");
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            LlenarRecaudoPorDocumento();
            btnAnular.BackColor = Color.LightGray;
            btnAnular.ForeColor = Color.Gray;
            btnAnular.Attributes.Add("disabled", "disabled");
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