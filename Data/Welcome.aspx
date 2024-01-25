<%@ Page Title="Welcome" Language="VB" MasterPageFile="~/Data/Site.master" AutoEventWireup="true" CodeFile="Welcome.aspx.vb" Inherits="_Welcome" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script type="text/javascript">
        $(document).ready(function () {
            $('aside.left-panel, section.content').hide();
        });
    </script>
</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <div class="row mt-2">
            <div class="col-md-12 r-head clearfix">
                <h3 class="h3 text-center">WELCOME TO EY MULTI STAKEHOLDER FEEDBACK!</h3>
            </div>
            <div class="col-md-8">
                <div class="section">
                    <p>Multi Stakeholder Feedback survey is a powerful mechanism to provide leaders with a deeper insight and understanding of their demonstrated leadership behaviours through a structured system to solicit feedback from key stakeholder groups (Superiors, peers, direct reports and stakeholders).</p>
                    <p>You have been nominated to provide feedback to one or more participants as part of the survey process.</p>
                    <p>Please be assured that your feedback will be anonymous and confidential. The participants will receive an average feedback score for each respondent group and not the individual responses. Further, the results of the survey will only be used for developmental purposes and will not impact the participant's performance appraisal in any way.</p>
                    <p>The survey will be open till <strong>Monday, 22<sup>nd</sup> November 2021 5 P.M.</strong> We request you to adhere to the timelines to ensure that the process is completed successfully on time.</p>
                    <p>Please click on the <strong>Provide Feedback</strong> tab to begin the feedback process</p>
                    <p>We look forward to your whole-hearted participation in this transformational exercise.</p>

                </div>
            </div>
            <div class="col-md-4">
                <img src="../Images/Group.jpg" class="img-thumbnail" />
            </div>

            <div class="col-md-12 alert alert-danger"><b>Note :</b> The entire feedback exercise is confidential and anonymous.</div>

            <div class="col-md-12 text-center">
                <asp:Button ID="btnContiue" runat="server" Text="Provide Feedback" CssClass="btns btn-submit" />
            </div>
        </div>
    </div>
</asp:Content>
