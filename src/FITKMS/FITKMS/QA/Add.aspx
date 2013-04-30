<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Add.aspx.cs" ValidateRequest="false" Inherits="FITKMS.QA.Add" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div class="dashboard-wrapper">
        <div class="main-container">
            <div id="success_label" class="alert alert-block alert-success fade in" runat="server" visible="false">
                <button data-dismiss="alert" class="close" type="button">
                    ×
                </button>
                <h4 class="alert-heading">Poruka!
                </h4>
                <p>
                    <asp:Label ID="successLabel" runat="server" Text="Label"></asp:Label>
                </p>
            </div>
            <div id="error_label" class="alert alert-block alert-error fade in" runat="server" visible="false">
                <button data-dismiss="alert" class="close" type="button">
                    ×
                </button>
                <h4 class="alert-heading">Greška!
                </h4>
                <p>
                    <asp:Label ID="errorLabel" runat="server" Text="Label"></asp:Label>
                </p>
            </div>
            <div class="widget no-margin">
                <div class="widget-header">
                    <div class="title">
                            <span class="fs1" aria-hidden="true" data-icon="&#xe022;"></span>
                            Novo pitanje
                         </div>
                    </div>
                <div class="row-fluid">
                    <div class="widget-body">
                        <div class="control-group">
                            <div class="controls controls-row">
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
                                    <ContentTemplate>
                                        <asp:DropDownList ID="ddOblast" runat="server" OnSelectedIndexChanged="ddOblast_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                        <asp:DropDownList ID="ddTema" runat="server"></asp:DropDownList>
                                        <asp:TextBox ID="txtNaslovPitanja" runat="server" CssClass="input-block-level" Placeholder="Naslov" required="required" x-moz-errormessage="Obavezno unesite naslov!"></asp:TextBox>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="ddOblast" EventName="SelectedIndexChanged" />
                                    </Triggers>
                                </asp:UpdatePanel>
                                <div class="wysiwyg-container">
                                    <asp:TextBox runat="server" ID="wysiwyg" class="span12" placeholder="Unesite pitanje ..." Style="height: 300px"></asp:TextBox>
                                </div>
                                <br />
                                <a href="#myModal" role="button" class="btn btn-warning2 " data-toggle="modal">Odaberite tagove
                                </a>
                                <div id="myModal" class="modal hide fade" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
                                    <div class="modal-header">
                                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                                            ×
                                        </button>
                                        <h4 id="myModalLabel">Izaberite tagove
                                        </h4>
                                    </div>
                                    <div class="modal-body">
                                        <asp:CheckBoxList ID="chkTagovi" runat="server" CssClass="CheckboxList"></asp:CheckBoxList>
                                    </div>

                                    <div class="modal-footer">
                                        <asp:Button ID="Button1" runat="server" Text="Button" class="btn btn-primary" OnClick="Button1_Click" />
                                    </div>
                                </div>
                                <asp:Button ID="Save" runat="server" Text="Postavi pitanje" class="btn btn-info" OnClick="Save_Click" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
