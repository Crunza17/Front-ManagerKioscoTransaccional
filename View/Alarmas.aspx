﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Alarmas.aspx.cs" Inherits="WebApplication2.WebForm1" EnableEventValidation="true" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<!-- 2022 - Touch Colombia - Todos los derechos reservados -->
<head runat="server">
<meta charset="utf-8" />
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
<meta name="viewport" content="width=device-width"/>
<meta http-equiv="refresh" content="300;URL=./Alarmas.aspx" />
<link rel="stylesheet" type="text/css" href="main.css" />
<link rel="stylesheet" href="https://unpkg.com/leaflet@1.7.1/dist/leaflet.css"
    integrity="sha512-xodZBNTC5n17Xt2atTPuE1HxjVMSvLVW9ocqUKLsCC5CXdbqCmblAshOMAS6/keqq/sMZMZ19scR4PsZChSR7A=="
   	crossorigin=""/>		
	<link rel="stylesheet" type="text/css" href="mapa.css"/>
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
<title>Alarmas</title>
</head>
<body>
    <form runat="server">
        <header class="p-3 mb-3 border-bottom">
            <div class="container">
              <div class="d-flex flex-wrap align-items-center justify-content-center justify-content-lg-start">
                <a href="https://www.touchcolombia.com/" class="d-flex align-items-center mb-2 mb-lg-0 text-dark text-decoration-none" title="© 2022 Copyright - Touch Colombia - Todos los derechos reservados">
                  <asp:Image ID="Image1" runat="server" ImageUrl="srcs/Png/Logo_Touch_Colombia.png" Height="80px" Width="150px" style="margin-right:50px;"/>
                </a>

                <ul class="nav col-12 col-lg-auto me-lg-auto mb-2 justify-content-center mb-md-0">
                    <div class="dropdown text-end dropdown-menu-end">
                        <a href="#" class="d-block link-dark text-decoration-none dropdown-toggle" id="A1" data-bs-toggle="dropdown" aria-expanded="false">
                            <asp:label ID="Label7" class="dropdown-item d-inline" runat="server" Text="Monitoreo"/>
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
                            <asp:label ID="Label8" class="dropdown-item d-inline" runat="server" Text="Transacciones"/>
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
            <section class="DGW-Box">
                    <div class="content-DGW-Alarm">
                    <asp:ScriptManager id="ScriptManager1" runat="server"></asp:ScriptManager>
                    <asp:UpdatePanel id="UpdatePanel1" UpdateMode="Conditional" runat="server">
                        <ContentTemplate>
                            <asp:GridView ID="GVAlarmas" class="table" runat="server" AutoGenerateColumns="false" Height="95%" Width="100%" CellPadding="5" OnSelectedIndexChanged="GVAlarmas_SelectedIndexChanged" HeaderStyle-HorizontalAlign="Center">
                                <Columns>
                                    <asp:commandfield HeaderText="Detalle" showselectbutton="true" buttontype="Image" SelectImageUrl="srcs\Png\logosimbolo.jpg" ControlStyle-Font-Underline="false" ControlStyle-ForeColor="Black" HeaderStyle-BackColor="#333333" HeaderStyle-ForeColor="White" HeaderStyle-Font-Size="20px"/>
                                    <asp:BoundField HeaderText="Kiosko" DataField="IdCajero" HeaderStyle-BackColor="#333333" HeaderStyle-ForeColor="White" HeaderStyle-Font-Size="20px" ItemStyle-HorizontalAlign="Center"/>
                                    <asp:ImageField HeaderText="Impresora" DataImageUrlField="Impresora" dataimageurlformatstring="~\srcs\Png\{0}.png" ControlStyle-Width="20px" ItemStyle-HorizontalAlign="Center" HeaderStyle-BackColor="#333333" HeaderStyle-ForeColor="White" HeaderStyle-Font-Size="20px"/>
                                    <asp:ImageField HeaderText="Datáfono" DataImageUrlField="BarcodeReader" dataimageurlformatstring="~\srcs\Png\{0}.png" ControlStyle-Width="20px" ItemStyle-HorizontalAlign="Center" HeaderStyle-BackColor="#333333" HeaderStyle-ForeColor="White" HeaderStyle-Font-Size="20px"/>
                                    <asp:ImageField HeaderText="Web" DataImageUrlField="WS" dataimageurlformatstring="~\srcs\Png\{0}.png" ControlStyle-Width="20px" ItemStyle-HorizontalAlign="Center" HeaderStyle-BackColor="#333333" HeaderStyle-ForeColor="White" HeaderStyle-Font-Size="20px"/>
                                    <asp:ImageField HeaderText="EnLinea" DataImageUrlField="EnLinea" dataimageurlformatstring="~\srcs\Png\{0}.png" ControlStyle-Width="20px" ItemStyle-HorizontalAlign="Center" HeaderStyle-BackColor="#333333" HeaderStyle-ForeColor="White" HeaderStyle-Font-Size="20px"/>
                                    <asp:BoundField HeaderText="Inactividad" DataField="Inactivo" HeaderStyle-BackColor="#333333" HeaderStyle-ForeColor="White" HeaderStyle-Font-Size="20px" ItemStyle-HorizontalAlign="Center"/>
                                </Columns>
                            </asp:GridView>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    </div>
                </section>
            <section class="content-headerA"> <!--- Cabecera de la caja -->
                    <button id="btnMapa" class="btn btn-dark" onclick="">Ir al Mapa</button>
                </section>
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

        <div class="modal-fade" id="ModalMap" aria-labelledby="ModalMap" aria-hidden="true">
              <div class="modal-body">
                  <div id="map"></div>
              </div>
            <section class="content-headerA"> <!--- Cabecera de la caja -->
                    <button id="btnSubir" class="btn btn-dark" onclick="">Volver a la tabla</button>
                </section>
        </div>


        <!-- Map API -->
            <script src="https://unpkg.com/leaflet@1.7.1/dist/leaflet.js"
   	        integrity="sha512-XQoYMqMTK8LvdxXYG3nZ448hOEQiglfqkJs1NOQV44cWnUrBc8PkAOcXy20w0vlaXaVUearIOBhiXZ5V3ynxwA=="
   	        crossorigin=""></script>   		
	
	        <script src="mapa.js"></script>
    </form>
</body>
</html>
