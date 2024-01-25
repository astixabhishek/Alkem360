<%@ Page Title="Questions" Language="VB" MasterPageFile="~/Data/Site.master" AutoEventWireup="false" CodeFile="frmQuestionMain.aspx.vb" Inherits="Questionaire_frmQuestionMain" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <link href="../Content/jquery-ui.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-ui.js" type="text/javascript"></script>

    <style type="text/css">
        html, body {
            max-width: 100%;
            overflow-x: hidden;
        }
    </style>

    <script type="text/javascript">
        $(document).ready(function () {
            $('aside.left-panel, section.content').hide();

            if (window.innerWidth < 768) {
                $(".r-head").css("margin-bottom", "4px");
                //$('.Questable').css('font-size', '6.5pt');
                //$("div.container-fluid").removeClass("container-fluid");
                //$("div.row").removeClass("row");
            }
        });
    </script>
    <script type="text/javascript">
        function fnMakeStringForSave() {
            var SelectedValue = "";
            //  debugger;  
            $("#dvSurvey").find("input[type=radio]:checked").each(function () {
                //fnshowDependentQstn(this);
                SelectedValue += ($(this).val()) + "^^|"
            });

            if (SelectedValue == "") {
                var RspDetID = $("#dvSurvey").find("input[type=radio]").eq(0).val().split("^")[0]
                SelectedValue = RspDetID + "^0^^|"
            }
            //alert(SelectedValue)
            return SelectedValue;
        }

        function fnChkValidation() {
            var chkRadioFlag = true;
            $("input:radio").each(function () {
                var name = $(this).attr("name");
                //alert("name=" + name)
                var $checked = $("input:radio[name=" + name + "]:checked").length;
                //   alert($checked)
                if ($checked == 0) {
                    alert("Please select atleast one option for each question")
                    $("input:radio[name=" + name + "]").eq(0).focus();
                    $("input:radio[name=" + name + "]").eq(0).addClass("RadioBackgrouncolor");
                    document.getElementById("btnNext").disabled = false;
                    chkRadioFlag = false;
                    return false;
                }
            });
            return chkRadioFlag;
        }

        function fnPrevious() {
            // debugger;
            document.getElementById("btnPrevious").disabled = true;
            var FinalSave = 2;
            // document.getElementById("dvLoadingImg").style.display = "block";
            var RspID = document.getElementById("MainContent_hdnRspID").value;
            var PGNmbr = document.getElementById("MainContent_hdnPGNmbr").value;
            var MaxPGNmbr = document.getElementById("MainContent_hdnMaxPGNmbr").value;

            if (PGNmbr < MaxPGNmbr) {
                var strRet = fnMakeStringForSave();
                var strComment = fnchkComments();
                //fnSaveData(FinalSave, strRet)
                fnSaveData(FinalSave, strRet, strComment, RspID)
            }
            else {
                fnSaveComments(2, 2)
            }
        }

        function fnSaveExit() {
            debugger;
            var PGNmbrCheck = document.getElementById("MainContent_hdnPGNmbr").value;
            if (window.confirm("Do you really want to exit")) {

                var FinalSave = 0;
                if (PGNmbrCheck != 8) {
                    var strComment = fnchkComments();
                    var strRet = fnMakeStringForSave();

                    var StringValue = "";
                    var SelectValue = "";
                    var isSame = true;
                    var previousVal = "";
                    for (var i = 0; i <= strRet.split("|").length - 2; i++) {
                        StringValue = strRet.split("|")[i];
                        SelectValue = StringValue.split("^")[1];
                        if (i == 0) {
                            previousVal = SelectValue;
                        }
                        if (previousVal != SelectValue) {
                            isSame = false;
                        }

                    }


                    if (isSame) {
                        if (window.confirm("You are provided the same rating across all questions. Are you sure that you want to proceed?")) {
                            var RspID = document.getElementById("MainContent_hdnRspID").value;
                            var PGNmbr = document.getElementById("MainContent_hdnPGNmbr").value;
                            var MaxPGNmbr = document.getElementById("MainContent_hdnMaxPGNmbr").value;
                            // debugger;
                            if (PGNmbr < MaxPGNmbr) {
                                var strRet = fnMakeStringForSave();
                                var strComment = fnchkComments();
                                //fnSaveData(FinalSave, strRet)
                                fnSaveData(FinalSave, strRet, strComment, RspID)
                            }
                            else {
                                fnSaveComments(2, 0)
                            }
                        }
                    }
                    else {
                        var RspID = document.getElementById("MainContent_hdnRspID").value;
                        var PGNmbr = document.getElementById("MainContent_hdnPGNmbr").value;
                        var MaxPGNmbr = document.getElementById("MainContent_hdnMaxPGNmbr").value;
                        debugger;
                        if (PGNmbr < MaxPGNmbr) {
                            var strRet = fnMakeStringForSave();
                            var strComment = fnchkComments();

                            //fnSaveData(FinalSave, strRet)
                            fnSaveData(FinalSave, strRet, strComment, RspID)
                        }
                        else {
                            fnSaveComments(2, 0)
                        }
                    }
                }
                else {
                    debugger;
                    var RspID = document.getElementById("MainContent_hdnRspID").value;
                    var PGNmbr = document.getElementById("MainContent_hdnPGNmbr").value;
                    var MaxPGNmbr = document.getElementById("MainContent_hdnMaxPGNmbr").value;
                    // debugger;
                    if (PGNmbr < MaxPGNmbr) {
                        var strRet = "";//fnMakeStringForSave();
                        var strComment = "";//fnchkComments();
                        //fnSaveData(FinalSave, strRet)
                        fnSaveData(FinalSave, strRet, strComment, RspID)
                    }
                    else {
                        fnSaveComments(2, 0)
                    }
                }



            }
        }



        function fnNext() {
            debugger;
            //alert("Hi11");
            if (fnChkValidation() == false) {
                return false;
            }
            document.getElementById("btnNext").disabled = true;
            var FinalSave = 1;
            var strRet = fnMakeStringForSave();
            //alert(strRet);
            //debugger;
            var StringValue = "";
            var SelectValue = "";
            var isSame = true;
            var previousVal = "";
            for (var i = 0; i <= strRet.split("|").length - 2; i++) {
                StringValue = strRet.split("|")[i];
                SelectValue = StringValue.split("^")[1];
                if (i == 0) {
                    previousVal = SelectValue;
                }
                if (previousVal != SelectValue) {
                    isSame = false;
                }

            }
            var PGNmbrCheck = document.getElementById("MainContent_hdnPGNmbr").value;
            if (PGNmbrCheck != 8) {
                if (isSame) {
                    if (window.confirm("You are provided the same rating across all questions. Are you sure that you want to proceed?")) {
                        var strComment = fnchkComments();
                        // document.getElementById("dvLoadingImg").style.display = "block";
                        var PGNmbr = document.getElementById("MainContent_hdnPGNmbr").value;
                        var MaxPGNmbr = document.getElementById("MainContent_hdnMaxPGNmbr").value;
                        var RspID = document.getElementById("MainContent_hdnRspID").value;
                        // debugger;
                        if (PGNmbr < MaxPGNmbr) {
                            // debugger;
                            fnSaveData(FinalSave, strRet, strComment, RspID)
                        }
                        else {
                            document.getElementById("dvQuestions").style.display = "none";
                            document.getElementById("dvSurvey").style.display = "none";
                            document.getElementById("divComments").style.display = "table";
                            document.getElementById("btnSubmit").style.display = "inline-block";
                            document.getElementById("btnNext").style.display = "none";
                            document.getElementById("tdPageNum1").innerHTML = "Page -" + parseInt(MaxPGNmbr) + "/" + parseInt(MaxPGNmbr);
                            E360WebService.fnGetRspComments(RspID, rsltGetCommentSuccess, rsltFailed);
                        }
                    }
                    else {
                        document.getElementById("btnNext").disabled = false;
                    }
                }
                else {

                    var strComment = fnchkComments();
                    // document.getElementById("dvLoadingImg").style.display = "block";
                    var PGNmbr = document.getElementById("MainContent_hdnPGNmbr").value;
                    var MaxPGNmbr = document.getElementById("MainContent_hdnMaxPGNmbr").value;
                    var RspID = document.getElementById("MainContent_hdnRspID").value;
                    // debugger;
                    if (PGNmbr < MaxPGNmbr) {
                        // debugger;
                        fnSaveData(FinalSave, strRet, strComment, RspID)
                    }
                    else {
                        document.getElementById("dvQuestions").style.display = "none";
                        document.getElementById("dvSurvey").style.display = "none";
                        document.getElementById("divComments").style.display = "table";
                        document.getElementById("btnSubmit").style.display = "inline-block";
                        document.getElementById("btnNext").style.display = "none";
                        document.getElementById("tdPageNum1").innerHTML = "Page -" + parseInt(MaxPGNmbr) + "/" + parseInt(MaxPGNmbr);
                        E360WebService.fnGetRspComments(RspID, rsltGetCommentSuccess, rsltFailed);
                    }
                }
            }



        }

        function FinalSubmit() {
            // debugger;
            if (window.confirm("Are you sure you want to submit?")) {
                fnSaveComments(1, 3)
            }
            else { }
        }

        function fnSaveData(FinalSave, strResult, strComment, RspID) {
            //alert("strResult=" + strResult)
            var FSave = FinalSave;
            E360WebService.fnUpdateResponses(strResult, FinalSave, strComment, RspID, rsltUpdateResponsesSuccess, rsltFailed, FSave);
        }

        function rsltUpdateResponsesSuccess(result, FinalSave) {
            /* debugger;*/
            //  alert(result)
            //alert(result.split("^")[3])
            if (result.split("@")[0] == 1) {
                if (FinalSave == 0) {
                    window.location.href = "../Data/frmMain.aspx?ChkFlg=1";
                    return false;
                }
                var PGNmbr = result.split("@")[1].split("^")[3]
                document.getElementById("MainContent_hdnPGNmbr").value = PGNmbr
                //alert("PGNmbr=" + PGNmbr)
                var RspID = document.getElementById("MainContent_hdnRspID").value;
                var MaxPGNmbr = document.getElementById("MainContent_hdnMaxPGNmbr").value;
                var levelID = document.getElementById("MainContent_hdnLevelID").value;
                /* debugger;*/
                document.getElementById("tdPageNum1").innerHTML = "Page -" + parseInt(PGNmbr) + "/" + parseInt(MaxPGNmbr);
                if (PGNmbr == 1) {
                    document.getElementById("btnPrevious").style.display = "none";
                }
                else {
                    document.getElementById("btnPrevious").style.display = "inline-block";
                }
                if (PGNmbr < MaxPGNmbr) {
                    // alert(levelID);
                    E360WebService.subPopulateQuestions(RspID, PGNmbr, levelID, rsltGetStatementSuccess, rsltFailed, PGNmbr);
                }
                else {
                    document.getElementById("dvQuestions").style.display = "none";
                    document.getElementById("dvSurvey").style.display = "none";
                    document.getElementById("divComments").style.display = "table";
                    document.getElementById("btnSubmit").style.display = "inline-block";
                    document.getElementById("btnNext").style.display = "none";
                    document.getElementById("tdPageNum1").innerHTML = "Page -" + parseInt(MaxPGNmbr) + "/" + parseInt(MaxPGNmbr);
                }
            }
        }

        function rsltGetStatementSuccess(result, PGNmbr) {
            //alert(result)
            if (result.split("@")[0] == 1) {
                document.getElementById("btnNext").disabled = false;
                document.getElementById("btnPrevious").disabled = false;
                document.getElementById("dvQuestions").style.display = "table-cell";
                document.getElementById("dvSurvey").style.display = "block";
                document.getElementById("divComments").style.display = "none";
                document.getElementById("btnSubmit").style.display = "none";
                document.getElementById("btnNext").style.display = "inline-block";
                var MaxPGNmbr = document.getElementById("MainContent_hdnMaxPGNmbr").value;
                document.getElementById("MainContent_hdnPGNmbr").value = PGNmbr;
                document.getElementById("tdPageNum1").innerHTML = "Page -" + parseInt(PGNmbr) + "/" + parseInt(MaxPGNmbr);
                $("#dvSurvey")[0].innerHTML = result.split("@")[1];
                //$("MainContent_hdnCmptncyID").value = result.split("@")[2];
                document.getElementById("MainContent_hdnCmptncyID").value = result.split("@")[2];
            }
        }

        function fnSaveComments(X, FinalSave) {
            if (X == "1") {
                if (document.getElementById("MainContent_Textbox1").value == "") {
                    alert("Please note that it is mandatory to fill the comment");
                    return false;
                }
                if (document.getElementById("MainContent_Textbox2").value == "") {
                    alert("Please note that it is mandatory to fill the comment");
                    return false;
                }
                if (document.getElementById("MainContent_Textbox3").value == "") {
                    alert("Please note that it is mandatory to fill the comment");
                    return false;
                }
            }
            var RspID = document.getElementById("MainContent_hdnRspID").value;
            E360WebService.fnRspInsertRspComments(RspID, document.getElementById("MainContent_Textbox1").value, document.getElementById("MainContent_Textbox2").value, document.getElementById("MainContent_Textbox3").value, FinalSave, rsltRspInsertRspComments, rsltFailed, FinalSave + "^" + RspID);
        }

        function rsltRspInsertRspComments(result, Rep) {
            /*   debugger;*/
            if (result.split("@")[0] == "1") {
                var FinalSave = Rep.split("^")[0]
                var RspID = Rep.split("^")[1]
                var PGNmbr = result.split("^")[3]
                var levelID = document.getElementById("MainContent_hdnLevelID").value;
                if (FinalSave == 0) {
                    window.location.href = "../Data/frmMain.aspx?ChkFlg=1";
                    return false;
                }
                if (FinalSave == 2) {
                    E360WebService.subPopulateQuestions(RspID, PGNmbr, levelID, rsltGetStatementSuccess, rsltFailed, PGNmbr);
                }
                else {
                    alert("Submitted Successfully")
                    window.location.href = "../Data/frmMain.aspx?ChkFlg=1";
                }
            }
        }

        function rsltFailed(result) {
            //if (result.split("@")[0] == "2") {
            //    alert(result.split("@")[1])
            //}
            alert("Oops! Something went wrong. Please try again.");
        }


        function fnCheckLengthAnsCmt(obj) {//debugger;
            //	alert("In Function")
            var strTextBox = ""
            if (obj.value.length >= 300) {
                strTextBox = obj.value;
                obj.value = strTextBox.substring(0, 300);
                alert("Text Can't be more than 700 characters");
                return false;
            }
        }

        function fnAdjustRows(textarea) {//debugger;
            if (document.all) {
                /*	alert("Scroll Height" + textarea.scrollHeight )
                    alert("textarea.clientHeight="  + textarea.clientHeight)*/
                while (textarea.scrollHeight > textarea.clientHeight)
                    textarea.rows++;
                textarea.scrollTop = 0;
            }
        }
        function fnClearTextbox(TextBoxID) {
            if (document.getElementById(TextBoxID).innerText == "Comments") {
                document.getElementById(TextBoxID).innerText = "";
            }
        }

        function fnchkComments() {
            // alert("In fn")
            var RspID = document.getElementById("MainContent_hdnRspID").value;
            debugger;
            //alert(RspID);
            var flag = 0;
            var cmptncyComments = "";
            var Comment = $("#dvSurvey").find("textarea").eq(0).val();
            // alert(Comment);
            var Cntr = document.getElementById("MainContent_hdnCmptncyCmntCntr").value;
            // alert("Cntr=" + Cntr)

            var str1 = "121|";
            var str = document.getElementById("MainContent_hdnCmptncyID").value;
            // alert(str1);
            //  alert(str);
            var cmpId = new Array();
            cmpId = str.split("|")
            //  alert(cmpId);
            for (var i = 1; i <= Cntr; i++) {

                var StrTxt;
                var ComntVAl = document.getElementById("txtCompCmt" + i).value
                //  alert(ComntVAl);
                //debugger;
                if (ComntVAl == "Comments") {

                    ComntVAl = "";
                    alert(ComntVAl);
                }
                /////-----------------------

                ////-------------------------
                if (cmptncyComments == "") {
                    if (ComntVAl != StrTxt) {
                        cmptncyComments = ComntVAl + "^" + cmpId[i - 1];
                    }
                    else {

                    }
                }
                else {
                    if (ComntVAl != StrTxt) {
                        cmptncyComments = cmptncyComments + "|" + ComntVAl + "^" + cmpId[i - 1];
                    }
                    else {

                    }
                }
                //alert(document.getElementById("txtCompCmt" + i).value);							
                //document.getElementById("hdnCmptncyCmntValue").value=document.getElementById("txtCompCmt" + i).value;
            }
            document.getElementById("MainContent_hdnCmptncyCmnt").value = cmptncyComments;

            // alert(cmptncyComments);
            return Comment + "@" + str;

        }




    </script>

    <script type="text/javascript">
        var specialKeys = new Array();
        //specialKeys.push(8);  //Backspace
        //specialKeys.push(9);  //Tab
        //specialKeys.push(46); //Delete
        //specialKeys.push(36); //Home
        //specialKeys.push(35); //End
        //specialKeys.push(37); //Left
        //specialKeys.push(39); //Right

        specialKeys.push(64); //Right
        specialKeys.push(94); //Right


        function IsAlphaNumeric(e) {
            //  alert("Porwal");
            var keyCode = e.keyCode == 0 ? e.charCode : e.keyCode;
            // alert(keyCode);
            // var ret = ((keyCode >= 48 && keyCode <= 57) || (keyCode >= 65 && keyCode <= 90) || (keyCode >= 97 && keyCode <= 122) || (specialKeys.indexOf(e.keyCode) != -1 && e.charCode != e.keyCode));

            var ret = ((specialKeys.indexOf(e.keyCode) == -1));
            // var ret = ((keyCode != 64) || (keyCode != 94));
            document.getElementById("error").style.display = ret ? "none" : "inline";
            return ret;
        }
    </script>

    <script type="text/javascript">
        $(document).ready(function () {
            /* debugger;*/
            var RspID = document.getElementById("MainContent_hdnRspID").value;
            var levelID = document.getElementById("MainContent_hdnLevelID").value;
            var PGNmbr = document.getElementById("MainContent_hdnPGNmbr").value;
            var MaxPGNmbr = document.getElementById("MainContent_hdnMaxPGNmbr").value;
            var Name = document.getElementById("MainContent_hdnMaxPGNmbr").value;

            //alert(RspID);
            //alert(PGNmbr);
            //alert(MaxPGNmbr);
            document.getElementById("dvName").innerHTML = "Feedback for:-" + document.getElementById("MainContent_hdnName").value
            if (PGNmbr == 1) {
                document.getElementById("btnPrevious").style.display = "none";
            }
            else {
                document.getElementById("btnPrevious").style.display = "inline-block";
            }
            if (PGNmbr < MaxPGNmbr) {
                document.getElementById("dvQuestions").style.display = "table-cell";
                document.getElementById("dvSurvey").style.display = "block";
                document.getElementById("divComments").style.display = "none";
                document.getElementById("btnSubmit").style.display = "none";
                document.getElementById("btnNext").style.display = "inline-block";
                document.getElementById("tdPageNum1").innerHTML = "Page -" + parseInt(PGNmbr) + "/" + parseInt(MaxPGNmbr);
                E360WebService.subPopulateQuestions(RspID, PGNmbr, levelID, rsltGetStatementSuccess, rsltFailed, PGNmbr);
            }
            else {
                document.getElementById("dvQuestions").style.display = "none";
                document.getElementById("dvSurvey").style.display = "none";
                document.getElementById("divComments").style.display = "table";
                document.getElementById("btnSubmit").style.display = "inline-block";
                document.getElementById("btnNext").style.display = "none";
                document.getElementById("tdPageNum1").innerHTML = "Page -" + parseInt(MaxPGNmbr) + "/" + parseInt(MaxPGNmbr);
                //  debugger;
                E360WebService.fnGetRspComments(RspID, rsltGetCommentSuccess, rsltFailed);
            }
        });

        function rsltGetCommentSuccess(result) {
            if (result.split("@")[0] == "1") {
                document.getElementById("MainContent_Textbox1").value = result.split("@")[1].split("^")[0]
                document.getElementById("MainContent_Textbox2").value = result.split("@")[1].split("^")[1]
                document.getElementById("MainContent_Textbox3").value = result.split("@")[1].split("^")[2]
            }
        }

        function fnCheckLength(obj) {
            //       alert("In Function")
            var strTextBox = ""
            if (obj.value.length >= 3000) {
                strTextBox = obj.value;
                obj.value = strTextBox.substring(0, 3000);
                alert("Text Can't be more than 3000 characters")
            }
        }
        function fninstruction() {
            $("#dvinstruction").dialog({
                title: 'Instruction',
                modal: true,
                width: '85%',
                maxHeight: $(window).height() - 75
                //minHeight: 150
            });
        }
    </script>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <!--Ques Heading-->
    <%--<div class="r-head clearfix w-100 ">
        <h3 class="h3">EY Multi Stakeholder Feedback for Senior Management</h3>
    </div>--%>
    <div class="row r-head pt-1 pb-1">
        <div class="col-md-11">
            <h3 class="h3">EY MULTI STAKEHOLDER FEEDBACK</h3>
        </div>
        <div class="col-md-1">
            <input type="button" class="btn btn-primary" id="btn-instruction" onclick="fninstruction()" value="Instructions" />
        </div>
    </div>
    <div class="container-fluid">
        <!--Ques top header-->
        <div id="tblTopHeader" class="ques-header row">
            <div class="col-md-6 ques-header-left">
                <table class="Questable">
                    <tr>
                        <th id="tdPageNum1">
                            <br>
                        </th>
                        <th id="dvName">
                            <br>
                        </th>
                        <th width="20%" id="dvRespColor">&nbsp;</th>
                        <th>&nbsp;</th>
                    </tr>
                </table>
            </div>
            <div class="col-md-6" id="dvQuestions">
                <table class="Questable">
                    <thead>
                        <tr>
                            <th class="text-center" style="width: 16.666px"><span id="Label1">Always<br>
                                (5)</span><br>
                            </th>
                            <th class="text-center" style="width: 16.666px"><span id="Label2">Almost Always<br>
                                (4)</span><br>
                            </th>
                            <th class="text-center" style="width: 16.666px"><span id="Label3">Sometimes<br>
                                (3)</span><br>
                            </th>
                            <th class="text-center" style="width: 16.666px"><span id="Label4">Rarely<br>
                                (2)</span><br>
                            </th>
                            <th class="text-center" style="width: 16.666px"><span id="Label5">Never<br>
                                (1)</span><br>
                            </th>
                            <th class="text-center" style="width: 16.666px"><span id="Label6">Not Applicable<br>
                                (0)</span><br>
                            </th>
                        </tr>
                    </thead>
                </table>
            </div>
        </div>
        <!--Ques top header end-->

        <!--Ques Section end-->
        <div id="dvSurvey" class="w-100"></div>

        <!--divComments Section end-->
        <div class="ques-row w-100" id="divComments" style="display: none; overflow-x: hidden; overflow-y: hidden;">
            <div class="row mb-2">
                <div class="col-md-6 align-right-middile" id="tdCmmnt1" runat="server">Please list down action the person needs to start doing</div>
                <div class="col-md-6">
                    <asp:TextBox onkeypress="fnCheckLength(this);" ID="Textbox1" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="4"></asp:TextBox>
                </div>
            </div>
            <div class="row mb-2">
                <div class="col-md-6 align-right-middile" id="tdCmmnt2" runat="server">Please list down actions the person needs continue doing</div>
                <div class="col-md-6">
                    <asp:TextBox onkeypress="fnCheckLength(this);" ID="Textbox2" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="4"></asp:TextBox>
                </div>
            </div>
            <div class="row mb-2">
                <div class="col-md-6 align-right-middile" id="tdCmmnt3" runat="server">Please list down actions the person needs to stop doing/ change</div>
                <div class="col-md-6">
                    <asp:TextBox onkeypress="fnCheckLength(this);" ID="Textbox3" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="4"></asp:TextBox>
                </div>
            </div>
        </div>
        <div class="row mt-3 mb-3">
            <div class="col-md-11">
                <div class="text-center">
                
                    <input type="button" class="btns btn-submit" id="btnPrevious" onclick="fnPrevious()" value="Previous" />
                    <input type="button" class="btns btn-submit" id="btnNext" onclick="fnNext()" value="Save & Continue" />
                    <input type="button" class="btns btn-submit" id="btnSubmit" onclick="FinalSubmit()" value="Submit" />
                  
                </div>
            </div>
            <div class="col-md-1 text-right">
                <input type="button" class="btns btn-submit" id="btnSaveExit" onclick="fnSaveExit()" value="Exit" />
            </div>
        </div>
    </div>

    <div id="dvinstruction" style="display: none;">
        <div class="r-head clearfix">
            <h3 class="h3 text-center">WELCOME TO EY MULTI STAKEHOLDER FEEDBACK!</h3>
        </div>
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
       <p>Please feel free to contact <strong>Tanvi Prashant Pathak</strong> at <a href = mailto:tanvi.pathak@alkem.com> tanvi.pathak@alkem.com </a>for any clarifications on the process. .</p>


    </div>
    </div>

    <input type="hidden" id="hdnRspID" runat="server" />
    <input type="hidden" id="hdnPGNmbr" runat="server" />
    <input type="hidden" id="hdnMaxPGNmbr" runat="server" />
    <input type="hidden" id="hdnName" runat="server" />
    <input type="hidden" id="hdnLevelID" runat="server" />
    <input type="hidden" id="hdnCmptncyCmntCntr" runat="server" name="hdnCmptncyCmntCntr">
    <input type="hidden" id="hdnCmptncyCmnt" runat="server" name="hdnCmptncyCmnt">
    <input type="hidden" id="hdnCmptncyCmntValue" runat="server" name="hdnCmptncyCmntValue">
    <input type="hidden" id="hdnCmptncyID" runat="server" name="hdnCmptncyID">
</asp:Content>
