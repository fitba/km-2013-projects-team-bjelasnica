<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="AllQuestions.aspx.cs" Inherits="FITKMS.QA.AllQuestions" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="dashboard-wrapper">
        <div class="main-container">


            <div class="row-fluid">
                <div class="span12">

             
                    <div style="border: 1px solid #F0F0F0; padding: 5px;">

                        <div style="border: none;">
                            <div class="container-fluid">

                                <div class="row-fluid">

                                    <div class="span8">

                                        <div class="widget no-margin">
                                            <div class="widget-header">
                                                <div class="title" style="font-size: 16px;">
                                                    <asp:Label ID="lblNaslovPitanja" runat="server" Text="Pitanja"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="widget-body">
                                                <div class="row-fluid">
                                                    <div class="span12">
                                                        <div class="row-fluid">
                                                        </div>
                                                        <div class="row-fluid">

                                                            <div class="span12">

                                                             
                                                                
                       <asp:DataGrid ID="pitanjaGrid" runat="server" AutoGenerateColumns="false" Width="100%" DataSource="<%# pitanja %>" ShowHeader="false"
                                    DataKeyField="PitanjeID" AllowPaging="true" AllowCustomPaging="true"
                                    GridLines="None"  PageSize="10" OnItemDataBound="pitanjaGrid_ItemDataBound" OnPageIndexChanged="pitanjaGrid_PageIndexChanged">
                                    <PagerStyle Mode="NumericPages" CssClass="pgr" />
                                    <Columns>
                                        <asp:TemplateColumn>
                                            <ItemTemplate>
                                                <a href='Questions.aspx?id=<%#Eval("PitanjeID")%>' style="font-size:16px;color:#1d4f79;"> <strong><%# Eval("Naslov") %>'</strong> </a>
                                                </strong>
                                                <br />
                                                <div style="text-align: justify; margin-top:5px;">
                                                    <asp:Literal ID="textLiteral" runat="server"></asp:Literal>
                                                </div>
                                           
                                                <p class="icomoon-small" style="margin-top:10px;">
                                                    <span class="fs1" aria-hidden="true" data-icon="&#xe070;"></span>by   
                                                    <asp:LinkButton ID="LinkButton2" runat="server" Text='<%# Eval("KorisnickoIme") %>'></asp:LinkButton>
                                                    | <span class="fs1" aria-hidden="true" data-icon="&#xe052;"></span>
                                                    <asp:Label ID="Label1" runat="server" Text='<%# string.Format("{0:dd.MM.yyyy}", Eval("DatumKreiranja")) %>'></asp:Label>
                                                    | <span class="fs1" aria-hidden="true" data-icon="&#xe1c3;"></span>
                                                    <asp:LinkButton ID="LinkButton3" runat="server" Text='<%# string.Format("{0} Odgovora", Eval("BrojOdgvora")) %>'></asp:LinkButton>
                                                    |  <span class="fs1" aria-hidden="true" data-icon="&#xe07e;"></span> 
                                                    <asp:LinkButton ID="LinkButton1" runat="server" Text='<%# Eval("BrojPregleda") %>'></asp:LinkButton>
                                                    | <span class="fs1" aria-hidden="true" data-icon="&#xe031;"></span>Tags:
                                                    <asp:Repeater ID="tagsRepeater" runat="server">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="tagLink" runat="server" CssClass="label label-info" Text='<%# Eval("Naziv") %>'></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                    <br />
                                                </p>
                                                <hr style="background-color:#cecece; color:#cecece; height:1px;"/>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                    </Columns>
                                </asp:DataGrid> 
                                                                
                                                                
                                                                
                                                                
                                                                
                                                                  
                                                                




                                                            </div>
                                                        </div>
                                                        <div class="row-fluid">
                                                            <div class="span12">
                                                         
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <!--vkraj pitnj-->
                                        <br />
                                        <!--početak pitanja-->

                                    </div>
                                    <div class="span4">
                                        <!-- početak tagova--->
                                        <div class="widget">
                                            <div class="widget-header">
                                                <div class="title">
                                                    <span class="fs1" aria-hidden="true" data-icon="&#xe031;"></span>Nova
                 
                                                </div>
                                            </div>
                                            <div class="widget-body">

                                                <p>Nova 1</p>
                                                <p>Nova 2</p>
                                                <p>Nova 3</p>
                                                <p>Nova 4</p>
                                                <p>Nova 5</p>
                                                <p>Nova 2</p>
                                                <p>Nova 3</p>
                                                <p>Nova 4</p>
                                                <p>Nova 5</p>
                                            </div>
                                        </div>
                                        <!-- kraj tagova--->

                                        <!-- početak tagova--->
                                        <div class="widget">
                                            <div class="widget-header">
                                                <div class="title">
                                                    <span class="fs1" aria-hidden="true" data-icon="&#xe0b5;"></span>Neodgovorena
                 
                                                </div>
                                            </div>
                                            <div class="widget-body">

                                                <p>Neodgovorena 1</p>
                                                <p>Neodgovorena 2</p>
                                                <p>Neodgovorena 3</p>
                                                <p>Neodgovorena 4</p>
                                                <p>Neodgovorena 5</p>
                                                <p>Neodgovorena 2</p>
                                                <p>Neodgovorena 3</p>
                                                <p>Neodgovorena 4</p>
                                                <p>Neodgovorena 5</p>
                                            </div>
                                        </div>
                                        <!-- kraj tagova--->

                                        <!-- kraj 4 diva--->
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
