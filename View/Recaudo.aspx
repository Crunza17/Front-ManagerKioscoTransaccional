<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Recaudo.aspx.cs" Inherits="WebApplication2.WebForm4" EnableEventValidation = "false" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<!-- 2022 - Touch Colombia - Todos los derechos reservados -->
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
<link rel="stylesheet" type="text/css" href="main.css" />
    <!-- Font Awesome -->
<link
  href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.0.0/css/all.min.css"
  rel="stylesheet"
/>
<!-- Google Fonts -->
<link
  href="https://fonts.googleapis.com/css?family=Roboto:300,400,500,700&display=swap"
  rel="stylesheet"
/>
<!-- MDB -->
<link
  href="https://cdnjs.cloudflare.com/ajax/libs/mdb-ui-kit/4.0.0/mdb.min.css"
  rel="stylesheet"
/>
<script src="https://cdn.jsdelivr.net/npm/@popperjs/core@2.10.2/dist/umd/popper.min.js" integrity="sha384-7+zCNj/IqJ95wo16oMtfsKbZ9ccEh31eOz1HGyDuCQ6wgnyJNSYdrPa03rtR1zdB" crossorigin="anonymous"></script>
<script src="https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/js/bootstrap.min.js" integrity="sha384-QJHtvGhmr9XOIpI6YVutG+2QOK9T+ZnN4kzFN1RtK3zEFEIsxhlmWl5/YESvpZ13" crossorigin="anonymous"></script>
    <script type="text/javascript">
        function cargando() {
            var buttonID = '<%= ImgLoad.ClientID %>'
        document.getElementById(buttonID).style.display = 'block';
    }

    function quitarCargando() {
        var buttonID = '<%= ImgLoad.ClientID %>'
        document.getElementById(buttonID).style.display = 'none';
    }
</script>
    <title>Monetarias</title>
</head>
<body>
    <form runat="server" defaultfocus="txtDoc">
        <header class="p-3 mb-3 border-bottom">
            <div class="container">
              <div class="d-flex flex-wrap align-items-center justify-content-center justify-content-lg-start">
                <a href="https://www.touchcolombia.com/" class="d-flex align-items-center mb-2 mb-lg-0 text-dark text-decoration-none" title="© 2022 Copyright - Touch Colombia - Todos los derechos reservados">
                  <asp:Image ID="Image1" runat="server" ImageUrl="srcs/Png/Logo_Touch_Colombia.png" Height="80px" Width="150px" style="margin-right:50px;"/>
                </a>

                <ul class="nav col-12 col-lg-auto me-lg-auto mb-2 justify-content-center mb-md-0">
                    <div class="dropdown text-end dropdown-menu-end">
                        <a href="#" class="d-block link-dark text-decoration-none dropdown-toggle" id="A1" data-bs-toggle="dropdown" aria-expanded="false">
                            <asp:label ID="Label1" class="dropdown-item d-inline" runat="server" Text="Monitoreo"/>
                        </a>
                        <ul class="dropdown-menu text-small" aria-labelledby="dropdownUser1" style="">
                            <li><hr class="dropdown-divider"></li>
                            <li><asp:LinkButton ID="LinkButton7" class="dropdown-item" runat="server" href="Alarmas.aspx">Alarmas</asp:LinkButton></li>
                            <li><asp:LinkButton ID="LinkButton5" class="dropdown-item" runat="server" href="DetalleAlarma.aspx">Detalle Alarma</asp:LinkButton></li>
                            <li><asp:LinkButton ID="LinkButton4" class="dropdown-item" runat="server" href="Logws.aspx">Logs</asp:LinkButton></li>
                        </ul>
                    </div>
                    <div class="dropdown text-end dropdown-menu-end">
                        <a href="#" class="d-block link-dark text-decoration-none dropdown-toggle" id="A2" data-bs-toggle="dropdown" aria-expanded="false">
                            <asp:label ID="Label4" class="dropdown-item d-inline" runat="server" Text="Transacciones"/>
                        </a>
                        <ul class="dropdown-menu text-small" aria-labelledby="dropdownUser1" style="">
                            <li><hr class="dropdown-divider"></li>
                            <li><asp:LinkButton ID="LinkButton3" class="dropdown-item" runat="server" href="Recaudo.aspx">Monetarias</asp:LinkButton></li>
                            <li><asp:LinkButton ID="LinkButton6" class="dropdown-item" runat="server" href="Transacciones.aspx">No Monetarias</asp:LinkButton></li>
                        </ul>
                    </div>
                    <div class="dropdown text-end dropdown-menu-end" style="margin-left: 25px;">
                        <asp:LinkButton ID="LinkButton8" class="d-block link-dark" runat="server" href="CrearUsuario.aspx">Gestionar Usuario</asp:LinkButton>
                    </div>
                </ul>


                <div class="dropdown text-end dropdown-menu-end ml-3">
                  <a href="#" class="d-block link-dark text-decoration-none dropdown-toggle" id="dropdownUser1" data-bs-toggle="dropdown" aria-expanded="false">
                    <asp:label ID="lblUser" class="dropdown-item d-inline" runat="server" Text="Usuario"/>
                    <img src="srcs/Png/usuario.png" alt="mdo" width="32" height="32" class="rounded-circle d-inline">
                  </a>
                  <ul class="dropdown-menu text-small" aria-labelledby="dropdownUser1" style="">
                    <li><hr class="dropdown-divider"></li>
                    <li><asp:Button ID="btnUser" class="dropdown-item" runat="server" Text="Cambiar Contraseña" data-bs-toggle="modal" data-bs-target="#exampleModal" OnClientClick="return false;"/></li>
                    <li><asp:Button ID="btnCerrar" class="dropdown-item" runat="server" Text="Cerrar Sesión" data-bs-toggle="modal" data-bs-target="#Modal2" OnClientClick="return false;"/></li>
                  </ul>
                </div>
              </div>
            </div>
          </header>
        <div class="content-box">
            <div class="contenido">
                <section class="content-headerL"> <!--- Cabecera de la caja -->
                    <section class="content-headerL2">
                        <asp:Label ID="Label7" runat="server" Text="Sedes"></asp:Label>
                        <asp:DropDownList ID="cmbsedes" class="cmbKiosko" runat="server" AutoPostBack="true" OnSelectedIndexChanged="cmbsedes_SelectedIndexChanged"></asp:DropDownList>
                        <asp:Label ID="Label6" runat="server" Text="Kiosco"></asp:Label>
                        <asp:DropDownList ID="cmbkio" class="cmbKiosko" runat="server"></asp:DropDownList>
                        <asp:Label ID="Label2" runat="server" Text="Estado"></asp:Label>
                        <asp:DropDownList ID="cmbestado" class="cmbestado" runat="server" ></asp:DropDownList>
                        <asp:CheckBox ID="ckbCancelado" runat="server" Text="Anulados"/>
                        <asp:CheckBox ID="cknNoCancelado" runat="server" Text="Sin Anular"/>
                    </section>
                    <section class="content-headerL2">
                        <asp:Label ID="Label3" runat="server" Text="Desde" class="sectext"></asp:Label>
                        <asp:TextBox ID="dtpFechaIni" runat="server" TextMode="Date" class="sectext"></asp:TextBox>
                        <asp:Label ID="Label5" runat="server" Text="Hasta" class="sectext"></asp:Label>
                        <asp:TextBox ID="dtpFechaF" runat="server" TextMode="Date" class="sectext"></asp:TextBox>
                        <asp:Button ID="btnConsultar" class="btn btn-dark" runat="server" Text="Consultar" OnClick="btnConsultar_Click" OnClientClick="this.style.pointerEvents = 'none'; cargando();" />
                        <div class="border border-1 d-flex p-4" style="z-index: 0;">
                          <div class="form-outline">
                            <asp:TextBox ID="txtDoc" class="form-control border border-1 d-inline" runat="server" TextMode="Search" placeholder="Por Documento" ></asp:TextBox>
                          </div>
                          <asp:Button ID="btnBuscar" class="btn btn-dark d-inline" runat="server" Text="Buscar" OnClientClick="this.style.pointerEvents = 'none'; cargando();" OnClick="btnBuscar_Click"/> 
                        </div>
                        <asp:ScriptManager id="ScriptManager1" runat="server"></asp:ScriptManager>
                        <asp:ImageButton ID="ImageButtonEX" class="" style="" runat="server" ImageUrl="https://img.icons8.com/color/48/000000/microsoft-excel-2019--v1.png" OnClick="btnExportar_Click" /> 
                    </section>
                </section>
                <section class="DGW-Box">
                    <div class="content-DGW-100Com">
                        <asp:UpdatePanel id="UpdatePanel1" UpdateMode="Conditional" runat="server">
                            <Triggers>
                                <asp:AsyncPostBackTrigger controlid="btnConsultar" eventname="Click" />
                                <asp:AsyncPostBackTrigger controlid="btnBuscar" eventname="Click" />
                            </Triggers>
                            <ContentTemplate>
                                <asp:GridView ID="GVRecaudo" runat="server" class="table" AutoGenerateColumns="false" Height="400px" Width="100%" SelectedIndex="0" CellPadding="5" OnSelectedIndexChanged="GVRecaudo_SelectedIndexChanged" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                                    <Columns>
                                        <asp:commandfield HeaderText=" " showselectbutton="true" buttontype="Image" SelectImageUrl="srcs\Png\select.png" ControlStyle-Font-Underline="false" ControlStyle-ForeColor="Black" HeaderStyle-BackColor="#333333" HeaderStyle-ForeColor="White" ItemStyle-VerticalAlign="Middle" HeaderStyle-Font-Size="20px" ItemStyle-HorizontalAlign="Center" ControlStyle-Height="30px" ControlStyle-Width="30px"/>
                                        <asp:BoundField HeaderText="ID" DataField="ID" HeaderStyle-BackColor="#333333" HeaderStyle-ForeColor="White" HeaderStyle-Font-Size="20px" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle"/>
                                        <asp:BoundField HeaderText="Kiosco" DataField="Cajero" HeaderStyle-BackColor="#333333" HeaderStyle-ForeColor="White" HeaderStyle-Font-Size="20px" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle"/>
                                        <asp:BoundField HeaderText="Sede" DataField="NomSede" HeaderStyle-BackColor="#333333" HeaderStyle-ForeColor="White" HeaderStyle-Font-Size="20px" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle"/>
                                        <asp:BoundField HeaderText="Cita" DataField="Cita" HeaderStyle-BackColor="#333333" HeaderStyle-ForeColor="White" HeaderStyle-Font-Size="20px" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle"/> 
                                        <asp:BoundField HeaderText="N. Reserva" DataField="Factura" HeaderStyle-BackColor="#333333" HeaderStyle-ForeColor="White" HeaderStyle-Font-Size="20px" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle"/>
                                        <asp:BoundField HeaderText="Especialidad" DataField="Apellidos" HeaderStyle-BackColor="#333333" HeaderStyle-ForeColor="White" HeaderStyle-Font-Size="20px" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle"/>
                                        <asp:BoundField HeaderText="Documento" DataField="Documento" HeaderStyle-BackColor="#333333" HeaderStyle-ForeColor="White" HeaderStyle-Font-Size="20px" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle"/>
                                        <asp:BoundField HeaderText="TipoDoc" DataField="TipoDoc" HeaderStyle-BackColor="#333333" HeaderStyle-ForeColor="White" HeaderStyle-Font-Size="20px" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle"/>
                                        <asp:BoundField HeaderText="Nombres" DataField="Nombres" HeaderStyle-BackColor="#333333" HeaderStyle-ForeColor="White" HeaderStyle-Font-Size="20px" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle"/>
                                        <asp:BoundField HeaderText="Valor" DataField="Valor" HeaderStyle-BackColor="#333333" HeaderStyle-ForeColor="White" HeaderStyle-Font-Size="20px" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle"/>
                                        <asp:BoundField HeaderText="Fecha y Hora" DataField="FechaHora" HeaderStyle-BackColor="#333333" HeaderStyle-ForeColor="White" HeaderStyle-Font-Size="20px" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle"><HeaderStyle Wrap="false" /> 
                                            </asp:BoundField>
                                        <asp:BoundField HeaderText="Fecha de Pago" DataField="FechaPago" HeaderStyle-BackColor="#333333" HeaderStyle-ForeColor="White" HeaderStyle-Font-Size="20px" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle"><HeaderStyle Wrap="false" /> 
                                            </asp:BoundField>
                                        <asp:BoundField HeaderText="Estado" DataField="Estado" HeaderStyle-BackColor="#333333" HeaderStyle-ForeColor="White" HeaderStyle-Font-Size="20px" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle"/> 
                                        <asp:BoundField HeaderText="Autorizacion" DataField="Autorizacion" HeaderStyle-BackColor="#333333" HeaderStyle-ForeColor="White" HeaderStyle-Font-Size="20px" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle"/>
                                        <asp:BoundField HeaderText="TipoCuenta" DataField="TipoCuenta" HeaderStyle-BackColor="#333333" HeaderStyle-ForeColor="White" HeaderStyle-Font-Size="20px" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle"/>
                                        <asp:BoundField HeaderText="Franquicia" DataField="Franquicia" HeaderStyle-BackColor="#333333" HeaderStyle-ForeColor="White" HeaderStyle-Font-Size="20px" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle"/>
                                        <asp:BoundField HeaderText="Tarjeta" DataField="Tarjeta" HeaderStyle-BackColor="#333333" HeaderStyle-ForeColor="White" HeaderStyle-Font-Size="20px" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle"/>
                                        <asp:BoundField HeaderText="Cambio Status" DataField="Status" HeaderStyle-BackColor="#333333" HeaderStyle-ForeColor="White" HeaderStyle-Font-Size="20px" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle"/>
                                        <asp:BoundField HeaderText="Anulado" DataField="Anulacion" HeaderStyle-BackColor="#333333" HeaderStyle-ForeColor="White" HeaderStyle-Font-Size="20px" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle"/>
                                        <asp:BoundField HeaderText="Usuario Anulación/Status" DataField="Usuario" HeaderStyle-BackColor="#333333" HeaderStyle-ForeColor="White" HeaderStyle-Font-Size="20px" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle"/>
                                        <asp:BoundField HeaderText="Fecha Anulación/Status" DataField="FechaAnu" HeaderStyle-BackColor="#333333" HeaderStyle-ForeColor="White" HeaderStyle-Font-Size="20px" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle"/>
                                        <asp:BoundField HeaderText="Observación Anulación/Status" DataField="Observacion" HeaderStyle-BackColor="#333333" HeaderStyle-ForeColor="White" HeaderStyle-Font-Size="20px" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle"/>
                                    </Columns>
                                </asp:GridView>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </section>
                <section class="content-headerA">
                        <asp:UpdatePanel id="UpdatePanel2" UpdateMode="Conditional" runat="server">
                            <Triggers>
                                    <asp:AsyncPostBackTrigger controlid="GVRecaudo" eventname="SelectedIndexChanged" />
                                    <asp:AsyncPostBackTrigger controlid="btnConsultar" eventname="Click" />
                                    <asp:AsyncPostBackTrigger controlid="btnBuscar" eventname="Click" />
                                </Triggers>
                            <ContentTemplate>
                                    <asp:Button ID="btnAnular" class="btn d-inline" data-bs-toggle="modal" data-bs-target="#Modal3" runat="server" Text="Anular" autopostback="true"/>
                                    <asp:Button ID="btnSta" class="btn d-inline" data-bs-toggle="modal" data-bs-target="#ModalStatus" runat="server" Text="Cambiar Status" autopostback="true"/>
                                </ContentTemplate>
                        </asp:UpdatePanel>
                        <asp:UpdatePanel id="UpdatePanel4" UpdateMode="Conditional" runat="server">
                            <Triggers>
                                    <asp:AsyncPostBackTrigger controlid="btnConsultar" eventname="Click" />
                                </Triggers>
                            <ContentTemplate>
                                    <asp:Label ID="lblValor" Text="" runat="server"></asp:Label>
                                </ContentTemplate>
                        </asp:UpdatePanel>
                </section>
            </div>
        </div>
        <div class="modal fade" id="Modal3" tabindex="-1" aria-labelledby="Modal3" aria-hidden="true">
            <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="H5">Anular Transacción</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    <asp:UpdatePanel id="UpdatePanel3" UpdateMode="Conditional" runat="server">
                        <Triggers>
                            <asp:AsyncPostBackTrigger controlid="GVRecaudo" eventname="SelectedIndexChanged" />
                        </Triggers>
                        <ContentTemplate>
                                <asp:Label ID="lblInfo" runat="server"></asp:Label>
                                <h5 class="modal-title" id="H4">Observaciones</h5>
                                <asp:TextBox id="TextArea" TextMode="multiline" Columns="50" Rows="5" runat="server" /><br />
                            </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <div class="modal-footer">
                <asp:Button ID="btnAnularOB" class="btn btn-success" runat="server" Text="Anular" OnClick="btnAnularOB_Click" />
                <asp:Button ID="Button4" class="btn btn-danger" runat="server" Text="Cancelar" data-bs-dismiss="modal" aria-label="Close" OnClientClick="return false;"/>
                </div>
            </div>
            </div>
        </div>
        <div class="modal fade" id="ModalStatus" tabindex="-1" aria-labelledby="ModalStatus" aria-hidden="true">
            <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="H6">Cambiar Status - Transacción</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    <asp:UpdatePanel id="UpdatePanel5" UpdateMode="Conditional" runat="server">
                        <Triggers>
                            <asp:AsyncPostBackTrigger controlid="GVRecaudo" eventname="SelectedIndexChanged" />
                        </Triggers>
                        <ContentTemplate>
                                <asp:Label ID="lblInfoS" runat="server"></asp:Label>
                                <h5 class="modal-title" id="H7">Observaciones</h5>
                                <asp:TextBox id="txtObservacionesStatus" TextMode="multiline" Columns="50" Rows="5" runat="server" /><br />
                            </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <div class="modal-footer">
                <asp:Button ID="btnStatus" class="btn btn-success" runat="server" Text="Cambiar Status" OnClick="btnStatus_Click"/>
                <asp:Button ID="Button6" class="btn btn-danger" runat="server" Text="Cancelar" data-bs-dismiss="modal" aria-label="Close" OnClientClick="return false;"/>
                </div>
            </div>
            </div>
        </div>
        <div class="modal fade" id="exampleModal" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
          <div class="modal-dialog">
            <div class="modal-content">
              <div class="modal-header">
                <h5 class="modal-title" id="H1">Cambiar Contraseña</h5><br />
                <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
              </div>
              <div class="modal-body">
                <p>Ingrese las siguientes credenciales para cambiar su contraseña:</p>
                <asp:TextBox ID="txtPassA" class="form-control" TextMode="Password" runat="server" placeholder="Antigua Contraseña" Text="*"></asp:TextBox><br />
                <asp:TextBox ID="txtPassN" class="form-control" TextMode="Password" runat="server" placeholder="Nueva Contraseña" Text="*"></asp:TextBox><br />
                <asp:TextBox ID="txtPassC" class="form-control" TextMode="Password" runat="server" placeholder="Confirmar Contraseña" aria-describedby="basic-addon2" Text="*"></asp:TextBox>
              </div>
              <div class="modal-footer">
                <asp:Button ID="btnCambiar" class="btn btn-success" runat="server" Text="Cambiar" data-bs-toggle="modal" data-bs-target="#Modal1" OnClientClick="return false;" />
                <asp:Button ID="Button3" class="btn btn-danger" runat="server" Text="Cancelar" data-bs-dismiss="modal" aria-label="Close" OnClientClick="return false;"/>
              </div>
            </div>
          </div>
        </div>
        <div class="modal fade" id="Modal1" tabindex="-1" aria-labelledby="Modal1" aria-hidden="true">
          <div class="modal-dialog">
            <div class="modal-content">
              <div class="modal-header">
                <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
              </div>
              <div class="modal-body">
                  <h5 class="modal-title" id="H3">¿Está seguro?</h5>
              </div>
              <div class="modal-footer">
                <asp:Button ID="btnSi" class="btn btn-success" runat="server" Text="Si" OnClick="btnCambiar_Click" />
                <asp:Button ID="btnNo" class="btn btn-danger" runat="server" Text="No" data-bs-dismiss="modal" aria-label="Close" OnClientClick="return false;"/>
              </div>
            </div>
          </div>
        </div>
        <div class="modal fade" id="Modal2" tabindex="-1" aria-labelledby="Modal2" aria-hidden="true">
          <div class="modal-dialog">
            <div class="modal-content">
              <div class="modal-header">
                <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
              </div>
              <div class="modal-body">
                  <h5 class="modal-title" id="H2">¿Está seguro de cerrar la sesión?</h5>
              </div>
              <div class="modal-footer">
                <asp:Button ID="Button1" class="btn btn-success" runat="server" Text="Si" OnClick="btnCerrar_Click" />
                <asp:Button ID="Button2" class="btn btn-danger" runat="server" Text="No" data-bs-dismiss="modal" aria-label="Close" OnClientClick="return false;"/>
              </div>
            </div>
          </div>
        </div>
        
        <div>
            <asp:Image ID="ImgLoad" runat="server" ImageUrl="srcs/Png/loading.gif" CssClass="ImgLoad"/>
        </div>
    </form>
</body>
</html>
