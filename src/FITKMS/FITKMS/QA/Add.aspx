<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Add.aspx.cs" ValidateRequest="false" Inherits="FITKMS.QA.Add" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type = "text/javascript">
        function ClientItemSelected(sender, e) {
            $get("<%=tagsInput.ClientID %>").value += ', ';
         }
      </script>
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
                        <span class="fs1" aria-hidden="true" data-icon="&#xe0f6;"></span>
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
                                <asp:TextBox ID="tagsInput" runat="server" CssClass="input-block-level" placeholder="Unesite tagove">
                                </asp:TextBox>
                                <ajaxToolkit:AutoCompleteExtender ServiceMethod="GetTagNames" MinimumPrefixLength="1"
                                    CompletionInterval="100" EnableCaching="False" CompletionSetCount="10" TargetControlID="tagsInput" DelimiterCharacters=", "
                                    ID="AutoCompleteExtender1" runat="server" FirstRowSelected="false" OnClientItemSelected="ClientItemSelected"
                                    ShowOnlyCurrentWordInCompletionListItem="True" CompletionListHighlightedItemCssClass="label label-important" CompletionListItemCssClass="label">
                                </ajaxToolkit:AutoCompleteExtender>
                                <asp:Button ID="Save" runat="server" Text="Sačuvaj" class="btn btn-info pull-right" OnClick="Save_Click" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
