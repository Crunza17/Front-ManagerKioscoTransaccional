<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CrearUsuario.aspx.cs" Inherits="WebApplication2.CrearUsuario" EnableEventValidation="false" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<!-- 2022 - Touch Colombia - Todos los derechos reservados -->
<head runat="server">
<meta http-equiv="content-type" content="text/html; charset=UTF-8" />
<meta name="viewport" content="width=device-width"/>
<meta name="description" content="@Html.Raw(ViewBag.description)" />
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
    <title>Crear Usuario</title>
</head>
<body>
    <form id="Form1" runat="server">
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
            <div class="contenido">
                <section class="content-headerA"> <!--- Cabecera de la caja -->
                </section>
                <section class="DGW-Box">
                    <div class="content-DGW-100Com">
                        <asp:ScriptManager id="ScriptManager1" runat="server"></asp:ScriptManager>
                        <asp:UpdatePanel id="UpdatePanel1" UpdateMode="Conditional" runat="server">
                            <ContentTemplate>
                                <asp:GridView ID="GVUsuarios" runat="server" class="table" AutoGenerateColumns="false" Height="400px" Width="100%" SelectedIndex="0" CellPadding="5" HeaderStyle-HorizontalAlign="Center" OnSelectedIndexChanged="GVUsuarios_SelectedIndexChanged">
                                    <Columns>
                                        <asp:commandfield HeaderText="" showselectbutton="true" buttontype="Image" SelectImageUrl="srcs\Png\select.png" ControlStyle-Font-Underline="false" ControlStyle-ForeColor="Black" HeaderStyle-BackColor="#333333" HeaderStyle-ForeColor="White" ItemStyle-VerticalAlign="Middle" HeaderStyle-Font-Size="20px" ItemStyle-HorizontalAlign="Center" ControlStyle-Height="30px" ControlStyle-Width="30px"/>
                                        <asp:BoundField HeaderText="Documento" DataField="ID" HeaderStyle-BackColor="#333333" HeaderStyle-ForeColor="White" HeaderStyle-Font-Size="20px" ItemStyle-HorizontalAlign="Center"/>
                                        <asp:BoundField HeaderText="Nombres" DataField="Nombres" HeaderStyle-BackColor="#333333" HeaderStyle-ForeColor="White" HeaderStyle-Font-Size="20px" ItemStyle-HorizontalAlign="Center"/>
                                        <asp:BoundField HeaderText="Apellidos" DataField="Apellidos" HeaderStyle-BackColor="#333333" HeaderStyle-ForeColor="White" HeaderStyle-Font-Size="20px" ItemStyle-HorizontalAlign="Center"/>
                                        <asp:BoundField HeaderText="Cargo" DataField="Cargo" HeaderStyle-BackColor="#333333" HeaderStyle-ForeColor="White" HeaderStyle-Font-Size="20px" ItemStyle-HorizontalAlign="Center"/>
                                        <asp:BoundField HeaderText="Activo" DataField="Activo" HeaderStyle-BackColor="#333333" HeaderStyle-ForeColor="White" HeaderStyle-Font-Size="20px" ItemStyle-HorizontalAlign="Center"/>
                                        <asp:BoundField HeaderText="Fecha Creación" DataField="Fecha" HeaderStyle-BackColor="#333333" HeaderStyle-ForeColor="White" HeaderStyle-Font-Size="20px"  ItemStyle-HorizontalAlign="Center" HeaderStyle-Wrap="false"/>                                    </Columns>
                                </asp:GridView>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </section>
                <section class="content-headerA"> <!--- Cabecera de la caja -->
                    <asp:Button ID="btnInsertar" class="btn btn-dark" runat="server" Text="Insertar" data-bs-toggle="modal" data-bs-target="#modalInsertar" OnClientClick="return false;"/>
                    <asp:Button ID="btnModificar" class="btn btn-dark" runat="server" Text="Modificar" data-bs-toggle="modal" data-bs-target="#modalActualizar" OnClientClick="return false;"/>
                    Ctrl + F para Buscar
                </section>
            </div>
        </div>
        <div class="modal fade" id="modalInsertar" tabindex="-1" aria-labelledby="modalInsertar" aria-hidden="true">
          <div class="modal-dialog">
            <div class="modal-content">
              <div class="modal-header">
                <h5 class="modal-title" id="H4">Insertar Usuarios</h5><br />
                <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
              </div>
              <div class="modal-body">
                <p>Ingrese los siguientes datos del usuario a insertar:</p>
                <asp:TextBox ID="txtinserID" class="form-control" runat="server" placeholder="ID"></asp:TextBox><br />
                <asp:TextBox ID="txtinserNom" class="form-control" runat="server" placeholder="Nombres"></asp:TextBox><br />
                <asp:TextBox ID="txtinserApell" class="form-control" runat="server" placeholder="Apellidos" ></asp:TextBox><br />
                <asp:TextBox ID="txtinserContra" class="form-control" TextMode="Password" runat="server" placeholder="Contraseña"></asp:TextBox><br />
                <asp:DropDownList ID="cmbinserCargo" class="form-control" runat="server" placeholder="Cargo"></asp:DropDownList>
              </div>
              <div class="modal-footer">
                <asp:Button ID="btnIns" class="btn btn-success" runat="server" Text="Insertar" OnClick="btnIns_Click"/>
                 <asp:Button ID="Button5" class="btn btn-danger" runat="server" Text="Cancelar" data-bs-dismiss="modal" aria-label="Close" OnClientClick="return false;"/>
              </div>
            </div>
          </div>
        </div>
        <div class="modal fade" id="modalActualizar" tabindex="-1" aria-labelledby="modalActualizar" aria-hidden="true">
          <div class="modal-dialog">
            <div class="modal-content">
              <div class="modal-header">
                <h5 class="modal-title" id="H5">Insertar Usuarios</h5><br />
                <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
              </div>
              <div class="modal-body">
                <p>Ingrese los siguientes datos del usuario a modificar:</p>
                  <asp:UpdatePanel id="UpdatePanel2" UpdateMode="Conditional" runat="server">
                    <Triggers>
                        <asp:AsyncPostBackTrigger controlid="GVUsuarios" eventname="SelectedIndexChanged" />
                    </Triggers>
                    <ContentTemplate>
                    <div class="input-group mb-3">
                        <span class="input-group-text bg-light">Documento</span>
                        <asp:TextBox ID="txtactID" class="form-control" runat="server" ReadOnly="true"></asp:TextBox><br />
                    </div>
                    <div class="input-group mb-3">
                        <span class="input-group-text bg-light">Nombres</span>
                        <asp:TextBox ID="txtactNom" class="form-control" runat="server"></asp:TextBox><br />
                    </div>
                    <div class="input-group mb-3">
                        <span class="input-group-text bg-light">Apellidos</span>
                        <asp:TextBox ID="txtactApell" class="form-control" runat="server"></asp:TextBox><br />
                    </div>
                    <div class="input-group mb-3">
                        <span class="input-group-text bg-light">Contraseña</span>
                        <asp:TextBox ID="txtactContra" class="form-control" runat="server"></asp:TextBox><br />
                    </div>
                    <div class="input-group mb-3">
                        <span class="input-group-text bg-light">Cargo</span>
                        <asp:DropDownList ID="cmbactCargo" class="form-control" runat="server"></asp:DropDownList><br />
                    </div>
                    <div class="input-group mb-3">
                        <span class="input-group-text bg-light">Estado</span>
                        <asp:DropDownList ID="cmbactActivo" class="form-control" runat="server"></asp:DropDownList>
                    </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
              </div>
              <div class="modal-footer">
                <asp:Button ID="btnAct" class="btn btn-success" runat="server" Text="Actualizar" OnClick="btnAct_Click" />
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
                <asp:Button ID="btnSi" class="btn btn-success" runat="server" Text="Si" />
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
    </form>
</body>
</html>
