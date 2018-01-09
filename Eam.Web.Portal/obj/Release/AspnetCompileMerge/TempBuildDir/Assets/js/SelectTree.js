function buildTree(toshow, tree, target, Url, Attr) {
    

    $('<div  id="' + toshow + '" style=" background: #ffffff;z-index:9999;position: absolute; left: 639px; top: 135px; display: none;border:1px solid #ccc;width:250px;"><ul id="' + tree + '" class="ztree"></ul> </div>').insertAfter("#" + target);;
    $.fn.zTree.destroy(tree);
    var setting = {
        callback: {
            onClick: function nodeClick(event, treeId, treeNode) {
                if (treeNode.isParent == false) {
                    console.log(treeNode);
                    $("#" + target).val(treeNode[Attr]);
                    $("#" + toshow).hide();
                }
            }
        }
    };
    //$("#" + toshow + 'Close').click(function () {
    //    $("#" + toshow).hide();
    //});
    $.post(Url, function (result) {
   
        $.fn.zTree.init($("#" + tree), setting, result);
    });
    $("#" + target).click(function () {
        xOffset = 0;//向右偏移量
        yOffset = 25;//向下偏移量
        console.log();
        $("#" + toshow).css("position", "absolute").css("left", $("#" + target).position().left + xOffset + "px") .css("top", $("#" + target).position().top + yOffset + "px").show();
        window.event ? window.event.cancelBubble = true : e.stopPropagation();
    });
    $("#" + toshow).mouseleave(function () {
        $("#" + toshow).hide();
    });

    //$(document).click(function () {
    //    function checkIn(id) {
    //        var yy = 20;   //偏移量
    //        var str = "";
    //        var x = window.event.clientX;
    //        var y = window.event.clientY;
    //        var obj = $("#" + id)[0];
    //        var left=x > obj.offsetLeft;
    //        var right = x < (obj.offsetLeft + obj.clientWidth);
    //        var top=y > (obj.offsetTop - yy);
    //        var down=y < (obj.offsetTop + obj.clientHeight)
    //        if (left && right &&  top&& down) {
    //            return true;
    //        } else {
    //            return false;
    //        }
    //    }
    //    var is = checkIn(toshow);
    //    if (!is) {
         
    //    }
    //});
}

function buildTreeEx(toshow, tree, target,target1, Url, Attr, Attr1) {


    $('<div  id="' + toshow + '" style=" background: #ffffff;z-index:9999;position: absolute; left: 639px; top: 135px; display: none;"><ul id="' + tree + '" class="ztree"></ul> </div>').insertAfter("#" + target);;
    $.fn.zTree.destroy(tree);
    var setting = {
        callback: {
            onClick: function nodeClick(event, treeId, treeNode) {
                if (treeNode.isParent == false) {
                    console.log(treeNode);
                    $("#" + target).val(treeNode[Attr]);
                    $("#" + target1).val(treeNode[Attr1]);
                    $("#" + toshow).hide();
                }
            }
        }
    };
    //$("#" + toshow + 'Close').click(function () {
    //    $("#" + toshow).hide();
    //});
    $.post(Url, function (result) {

        $.fn.zTree.init($("#" + tree), setting, result);
    });
    $("#" + target).click(function () {
        xOffset = 0;//向右偏移量
        yOffset = 25;//向下偏移量
        console.log();
        $("#" + toshow).css("position", "absolute").css("left", $("#" + target).position().left + xOffset + "px").css("top", $("#" + target).position().top + yOffset + "px").show();
        window.event ? window.event.cancelBubble = true : e.stopPropagation();
    });
    $("#" + toshow).mouseleave(function () {
        $("#" + toshow).hide();
    });

    //$(document).click(function () {
    //    function checkIn(id) {
    //        var yy = 20;   //偏移量
    //        var str = "";
    //        var x = window.event.clientX;
    //        var y = window.event.clientY;
    //        var obj = $("#" + id)[0];
    //        var left=x > obj.offsetLeft;
    //        var right = x < (obj.offsetLeft + obj.clientWidth);
    //        var top=y > (obj.offsetTop - yy);
    //        var down=y < (obj.offsetTop + obj.clientHeight)
    //        if (left && right &&  top&& down) {
    //            return true;
    //        } else {
    //            return false;
    //        }
    //    }
    //    var is = checkIn(toshow);
    //    if (!is) {

    //    }
    //});
}