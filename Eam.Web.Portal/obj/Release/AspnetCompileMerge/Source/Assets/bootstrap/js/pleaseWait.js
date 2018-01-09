/**
 * Displays overlay with "Please wait" text. Based on bootstrap modal. Contains animated progress bar.
 */
function showPleaseWait() {
    var modalLoading = '<div class="modal" id="pleaseWaitDialog" data-backdrop="static" data-keyboard="false role="dialog">\
        <div class="modal-dialog">\
            <div class="modal-content">\
                <div class="modal-header">\
                    <h4 id="progressTitle" class="modal-title" >请稍等...</h4>\
                </div>\
                <div class="modal-body">\
                <div id="progressLab" class="progress-lable pull-right" style="color: #000080;font-weight: bold;font-size=36px">上传99%</div>\
                    <div class="progress">\
                       <div id="progressBar" class="progress-bar progress-bar-success progress-bar-striped active" role="progressbar"\
                      aria-valuenow="100" aria-valuemin="0" aria-valuemax="0" style="width:0%; height: 40px">\
                      </div>\
                    </div>\
                <span id="spanInfo1" ></span>\
                </br>\
                <span id="spanInfo2" ></span>\
                </div>\
            </div>\
        </div>\
    </div>';
    $(document.body).append(modalLoading);
    $("#pleaseWaitDialog").modal("show");
}

/**
 * Hides "Please wait" overlay. See function showPleaseWait().
 */

function hidePleaseWait() {
    $("#pleaseWaitDialog").modal("hide");
}

function showProgress(percentComplete, numUploaded, totalUpload, numImported, totalImported) {
    var infoFile = "共" + totalUpload + "Kb /已上传" + numUploaded + "Kb";
    var infoAssets = "总资产" + totalImported + "条 /已录入" + numImported + "条";
    $("#progressBar").width(percentComplete+'%');
    $("#progressLab").html(percentComplete + '%');
    $("#spanInfo1").html(infoFile);
    $("#spanInfo2").html(infoAssets);
    
  
  
}