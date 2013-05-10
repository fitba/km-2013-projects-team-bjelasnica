<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="FITKMS.Admin.Index" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="dashboard-wrapper">
        <div class="main-container">
            <div class="row-fluid">
                <div class="span8">
                    <div class="widget-body">
                        <div class="widget">
                            <div class="widget-body">
                                <ul class="nav nav-tabs no-margin myTabBeauty">
                                    <li class="active" runat="server" id="areasTab">
                                        <a data-toggle="tab" href="#areas">Oblasti
                                        </a>
                                    </li>
                                    <li class="" runat="server" id="themesTab">
                                        <a data-toggle="tab" href="#themes">Teme
                                        </a>
                                    </li>
                                    <li class="" runat="server" id="tagsTab">
                                        <a data-toggle="tab" href="#tags">Tagovi
                                        </a>
                                    </li>
                                </ul>
                                <div class="tab-content" id="myTabContent">
                                    <div id="areas" class="tab-pane fade acitve in" runat="server">
                                            <asp:Label ID="labelAreaInput" runat="server" ForeColor="Red" Visible="false" Text="Unesite naziv oblasti!"></asp:Label>
                                        <div class="input-append">
                                            <asp:TextBox ID="areaInput" CssClass="span6" PlaceHolder="Unesite naziv oblasti" runat="server"></asp:TextBox>
                                            <asp:Button ID="areaSubmit" runat="server" CssClass="btn btn-info" Text="Dodaj" OnClick="areaSubmit_Click"></asp:Button>
                                        </div>
                                        <asp:DataGrid runat="server" ID="areasGrid" AutoGenerateColumns="false" GridLines="None" Width="100%"
                                            CssClass="table table-condensed table-striped table-bordered table-hover no-margin" OnItemCommand="areasGrid_ItemCommand">
                                            <HeaderStyle Font-Bold="true" />
                                            <Columns>
                                                <asp:BoundColumn DataField="Naziv" HeaderText="Naziv oblasti" />
                                                <asp:TemplateColumn HeaderStyle-Width="15px">
                                                    <ItemTemplate>
                                                        <asp:LinkButton CommandName="deleteArea" CommandArgument='<%# Eval("OblastID") %>' ToolTip="Ukloni oblast" CssClass="icon-trash" runat="server"></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                            </Columns>
                                        </asp:DataGrid>
                                    </div>
                                    <div id="themes" class="tab-pane fade" runat="server">
                                        <asp:DropDownList ID="listAreas" CssClass="span6" runat="server" AutoPostBack="true" OnSelectedIndexChanged="listAreas_SelectedIndexChanged"></asp:DropDownList><br/>
                                        <asp:Label ID="labelThemeInput" runat="server" ForeColor="Red" Visible="false" Text="Unesite naziv teme!"></asp:Label>
                                        <div class="input-append">
                                            <asp:TextBox ID="themeInput" CssClass="span6" PlaceHolder="Unesite naziv teme" runat="server"></asp:TextBox>
                                            <asp:Button ID="themeSubmit" runat="server" CssClass="btn btn-info" Text="Dodaj" OnClick="themeSubmit_Click"></asp:Button>
                                        </div>
                                        <asp:DataGrid runat="server" ID="themesGrid" AutoGenerateColumns="false" GridLines="None" Width="100%"
                                            CssClass="table table-condensed table-striped table-bordered table-hover no-margin" OnItemCommand="themesGrid_ItemCommand">
                                            <HeaderStyle Font-Bold="true" />
                                            <Columns>
                                                <asp:BoundColumn DataField="Naziv" HeaderText="Naziv teme" />
                                                <asp:TemplateColumn HeaderStyle-Width="15px">
                                                    <ItemTemplate>
                                                        <asp:LinkButton CommandName="deleteTheme" CommandArgument='<%# Eval("TemaID") %>' ToolTip="Ukloni temu" CssClass="icon-trash" runat="server"></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                            </Columns>
                                        </asp:DataGrid>
                                    </div>
                                    <div id="tags" class="tab-pane fade" runat="server">
                                        <asp:Label ID="labelTagInput" runat="server" ForeColor="Red" Visible="false" Text="Unesite naziv taga!"></asp:Label><br />
                                        <asp:TextBox ID="tagInput" CssClass="span6" PlaceHolder="Unesite naziv taga" runat="server"></asp:TextBox><br />
                                        <asp:TextBox ID="tagDescInput" CssClass="span6" PlaceHolder="Unesite opis taga" TextMode="Multiline" runat="server"></asp:TextBox>
                                        <asp:Button ID="tagSubmit" runat="server" CssClass="btn btn-info" Text="Dodaj" OnClick="tagSubmit_Click"></asp:Button>
                                        <asp:DataGrid runat="server" ID="tagsGrid" AutoGenerateColumns="false" GridLines="None" Width="100%"
                                            CssClass="table table-condensed table-striped table-bordered table-hover no-margin" OnItemCommand="tagsGrid_ItemCommand">
                                            <HeaderStyle Font-Bold="true" />
                                            <Columns>
                                                <asp:BoundColumn DataField="Naziv" HeaderText="Naziv taga" />
                                                <asp:TemplateColumn HeaderStyle-Width="15px">
                                                    <ItemTemplate>
                                                        <asp:LinkButton CommandName="deleteTag" CommandArgument='<%# Eval("TagID") %>' ToolTip="Ukloni tag" CssClass="icon-trash" runat="server"></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                            </Columns>
                                        </asp:DataGrid>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
