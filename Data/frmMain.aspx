<%@ Page Title="Main Content" Language="VB" MasterPageFile="~/Data/Site.master" AutoEventWireup="false" CodeFile="frmMain.aspx.vb" Inherits="_frmMain" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script type="text/javascript">
        function fnAction(HierId) {
            if (HierId == 7) {
                $("#dvInstructions, #dvStatus").hide();
                $("aside.left-panel").removeClass('collapsed');
                E360WebService.fnShowDispPerson(fnShowDispPersonSuccess, fnShowDispPersonFail);

            }
            else if (HierId == 22) {
                $("#dvInstructions, #dvDispPerson").hide();
                $("aside.left-panel").removeClass('collapsed');
                E360WebService.fnGetCompletionStation(fnShowCompletionSuccess, fnShowCompletionStatusFail);
            }
            else if (HierId == 14) {
                $("#dvInstructions, #dvDispPerson,#dvStatus").hide();
                $("aside.left-panel").removeClass('collapsed');
                $("#heading").html("Daily Access Report");
                parent.FrmDetails.location.href = "../AdminReports/frmSystemAccessReport.aspx"
            }
            else if (HierId == 15) {
                $("#dvInstructions, #dvDispPerson,#dvStatus").hide();
                $("aside.left-panel").removeClass('collapsed');
                $("#heading").html("Daily Feedback Status");
                parent.FrmDetails.location.href = "../AdminReports/frmDailyFeedbackStatusReport.aspx"
            }
            else if (HierId == 10) {
                $("#dvInstructions, #dvDispPerson,#dvStatus").hide();
                $("aside.left-panel").removeClass('collapsed');
                $("#heading").html("Status By Appraisee");
                parent.FrmDetails.location.href = "../AdminReports/frmAppraiseeStatusReport.aspx"
            }
            else if (HierId == 11) {
                $("#dvInstructions, #dvDispPerson,#dvStatus").hide();
                $("aside.left-panel").removeClass('collapsed');
                $("#heading").html("Status By Appraiser");
                parent.FrmDetails.location.href = "../AdminReports/frmAppraiserStatusReport.aspx"
            }
        }

        function fnShowDispPersonSuccess(result) {
            if (result.split("@")[0] == "1") {
                $("#dvDispPerson").show();
                $("#heading").html("Provide Feedback")
                $("#dvDispPersonInner")[0].innerHTML = result.split("@")[1];

            }
        }
        function fnShowDispPersonFail(result) {
            if (result.split("@")[0] == "2") {
                $("#dvDispPersonInner")[0].innerHTML = result.split("@")[1];

            }
        }

        function fnShowCompletionSuccess(result) {
            if (result.split("@")[0] == "1") {
                $("#dvStatus").show();
                $("#heading").html("My feedback completion Status.")
                var returnRslt = result.split("@")[1];
                $("#tdComplete").html(returnRslt.split("^")[0]);
                $("#tdIncomplete").html(returnRslt.split("^")[1]);
                $("#tdPending").html(returnRslt.split("^")[2]);

            }
        }
        function fnShowCompletionStatusFail(result) {
            if (result.split("@")[0] == "2") {
                $("#dvStatusInner")[0].innerHTML = result.split("@")[1];

            }
        }

    </script>
    <script type="text/javascript">          
        var Type;

        function fnShowQuestion(sender, hdnRspId, hdnRspStatus, hdnCycApseApsrAssmntTypeMapID, hdnCycleID, hdnAssmntTypeID, hdnLevelID) {
            Type = 1;    //For Pre-Nominated Appraisals
            //var TempName = Name;
            //while (TempName.indexOf("_") != -1) {
            //    TempName = TempName.replace(new RegExp("_"), " ");
            //}
            var Name = $(sender).attr("sname");
            var strName = Name;
            strName = strName.replace(" ", "_");

            if (document.getElementById("hdnRspIdDis").value == "") {
                document.getElementById("hdnRspIdDis").value = hdnRspId
                window.location.href = "../Questionaire/Default.aspx?RspId=" + hdnRspId + "&RspStatus=" + hdnRspStatus + "&CycleApseApsrMapID=" + hdnCycApseApsrAssmntTypeMapID + "&CycleID=" + hdnCycleID + "&AssmntTypeID=" + hdnAssmntTypeID + "&LevelID=" + hdnLevelID + "&Name=" + strName + "&Type=" + Type

            }
            else {
                fnDisabled(strName)
            }
        }

        function fnDisabled(Name) {
            while (Name.indexOf("_") != -1) {
                Name = Name.replace(new RegExp("_"), " ");
            }

            alert("Feedback for  " + Name + "  has not been opened as yet another popup window is already opened")
            return false;

        }


    </script>
    <script type="text/javascript">

        $(document).ready(function () {
            $("body").css("overflow","hidden");

            if (document.getElementById("RightContent_hdnChkFlg").value == "1") {
                $("#dvInstructions, #dvStatus").hide();
                $("aside.left-panel").removeClass('collapsed');
                E360WebService.fnShowDispPerson(fnShowDispPersonSuccess, fnShowDispPersonFail);
            }

            var h = $("nav.navbar").outerHeight() + $(".r-head").outerHeight();
            $("#FrmDetails").css({
                width: "100%",
                height: $(window).height() - (h + 30),
                "overflow-y": "auto",
                "border": "none"
            });

            $("#dvInstructions, #dvDispPerson, #dvStatus").css({
                width: "100%",
                height: $(window).height() - (h + 35),
                "overflow-y": "auto",
                "border": "none"
            });

        });

    </script>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="Server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="LeftContent" runat="Server">
    <!-- Preloader -->
    <nav class="navigation" id="dvMain" runat="server"></nav>
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="RightContent" runat="Server">
    <div class="row r-head no-gutters">
        <button type="button" class="btn navbar-toggle col-1">
            <%--<span class="sr-only">Toggle navigation</span>--%>
            <span class="fa fa-bars"></span>
        </button>
        <h3 class="h3 col-11" id="heading">Instructions: </h3>
    </div>
    <div id="dvInstructions">
        <div class="container-fluid">
            <div class="section">
                <ol type="1">
                    <li>Below is a list of participants who have nominated you to provide feedback about them. The link attached along with displays the current status of the feedback survey for each of the participants.</li>
                    <li>Please click on the link next to the participants’ name for whom you wish to provide feedback to start the survey.</li>
                    <li>You will be able to provide objective feedback for each participant on a scale of 0-5.</li>
                    <li>The survey is divided into 8 sections. As you complete a section and move onto the next, the responses of the completed section are saved in the system automatically.</li>
                    <li>If you happen to leave the survey for any reason, only responses in the current section (in progress/ not completed section) will have to be re-filled.</li>
                    <li>There will be a few open-ended questions at the end of each section and at the end of the questionnaire. There is no word limit for these questions and we would encourage you to respond to them in as much detail as you wish.</li>
                    <li>Once you have filled in responses to all feedback statements, click on submit to complete the participant’s form. Please do not click on submit before answering all questions.</li>
                    <li>In case you do not wish to provide feedback for one or more Participants, you can choose to skip the same.</li>
                    <li>There is no time limit to complete the survey.</li>
                    <li>Please be honest and candid with your feedback. Please note that your feedback will be kept and confidential. Participants will receive average feedback score for each respondent group and not individual responses. </li>
                    <li>In case a behaviour has not been observed by you, please select "Not Applicable". Selecting this will not impact the average score computation.
You can access the instructions through a botton provided on the top right of the feedback survey screen</li>
                </ol>
                <p>Please feel free to contact <strong>Tanvi Prashant Pathak</strong> at <a href="mailto:tanvi.pathak@alkem.com">tanvi.pathak@alkem.com </a>for any clarifications on the process. .</p>

            </div>
        </div>
    </div>

    <div id="dvDispPerson" style="display: none">
        <div class="container-fluid" id="dvDispPersonInner"></div>
        <%--<<div class="clearfix col-md-12">
            div class="alert alert-danger"><b>Note : </b><i>Please ensure that Pop Ups are not blocked in your browser as the feedback form will open in pop up window</i></div>
        </div>--%>
    </div>

    <div id="dvStatus" style="display: none">
        <div class="container-fluid" id="dvStatusInner">
            <h3 class="h3">Below is the updated status of feedbacks that you have to take</h3>
            <table class='table table-bordered'>
                <thead>
                    <tr>
                        <th>S.No.</th>
                        <th>Results</th>
                        <th>Status</th>
                    </tr>
                </thead>
                <tbody>
                    <tr>
                        <td>1.</td>
                        <td id="tdCompleted1" class="Completed"><span class="fa fa-check"></span>&nbsp; Completed</td>
                        <td id="tdComplete"></td>
                    </tr>
                    <tr>
                        <td>2.</td>
                        <td id="tdIncompleted1" class="Inprogress"><span class="fa fa-spinner"></span>&nbsp; Inprogress</td>
                        <td id="tdIncomplete"></td>
                    </tr>
                    <tr>
                        <td>3.</td>
                        <td id="tdPending1" class="Not-Start"><span class="fa fa-times"></span>&nbsp; Not Started</td>
                        <td id="tdPending"></td>
                    </tr>
                </tbody>
            </table>
        </div>

    </div>
    <div class="container-fluid">
        <div class="section">
            <iframe id="FrmDetails" name="FrmDetails"></iframe>
        </div>
    </div>
    <input type="hidden" id="hdnRspIdDis" />
    <input type="hidden" id="hdnChkFlg" value="0" runat="server" />

    <%--<iframe name="frmRight" id="frmRight" frameborder="0" style="overflow: hidden; height: 100%; width: 100%"></iframe>--%>
</asp:Content>

