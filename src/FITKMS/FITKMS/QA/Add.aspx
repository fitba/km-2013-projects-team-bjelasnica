<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Add.aspx.cs" Inherits="FITKMS.QA.Add" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div class="container-fluid">


        <div class="dashboard-wrapper">
            <div class="main-container">


                <div class="row-fluid">
                    <div class="span12">
                        <div class="widget no-margin">

                            <div class="widget-body">
                                <div class="container-fluid">

                                    <div class="row-fluid">

                                        <div class="span12">


                                            <!--početak pitanja-->
                                            <!-- p pitanja -->

                                            <asp:TextBox ID="txtNaslovPitanja" runat="server" placeholder="Unesite naslov pitanja" Style="width: 97%; height: 40px; margin: 5px 5px 5px 15px; border: none; font-size: 20px;"></asp:TextBox>
                                            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
                                                <ContentTemplate>
                                                    <div style="height: 35px; margin-top: 8px;">
                                                        <div style="float: left; width: 90px;">
                                                            <label style="margin-top: 4px; margin-left: 15px; margin-right: 8px;" runat="server">
                                                                Oblast:
                                                            </label>
                                                        </div>

                                                        <div style="float: left;">
                                                            <asp:DropDownList ID="ddOblast" runat="server" OnSelectedIndexChanged="ddOblast_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                                        </div>
                                                    </div>

                                                    <div style="height: 35px; margin-top: 8px;">
                                                        <div style="float: left; width: 90px;">
                                                            <label style="margin-top: 4px; margin-left: 15px; margin-right: 8px;">
                                                                Tema:
                     
                                                            </label>
                                                        </div>
                                                        <div style="float: left;">

                                                            <asp:DropDownList ID="ddTema" runat="server"></asp:DropDownList>

                                                        </div>
                                                    </div>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="ddOblast" EventName="SelectedIndexChanged" />
                                                </Triggers>
                                            </asp:UpdatePanel>



                                            <div class="widget-body">

                                                <div class="wysiwyg-container">

                                                    <asp:TextBox runat="server" ID="wysiwyg" class="span12" placeholder="Unesite pitanje ..." Style="height: 300px"></asp:TextBox>
                                                </div>


                                                <br />
                                                <a href="#myModal" role="button" class="btn btn-warning2 " data-toggle="modal">Odaberite tagove
                  </a>

                                                <!-- Modal -->
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

                                                <asp:Button ID="Save" runat="server" Text="Postavi pitanje" class="btn btn-info" OnClick="Save_Click"/>
                             
                               
                                            </div>
                                            <!--kraj pitanja-->



                                            <!-- kraj glavnog diva-->
                                        </div>
                                        <div class="span4">
                                            <!-- početak tagova--->


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

        <!--/.fluid-container-->
    </div>
</asp:Content>
