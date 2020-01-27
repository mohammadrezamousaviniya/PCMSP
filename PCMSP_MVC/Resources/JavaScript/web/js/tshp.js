function inputJsonConverter(idofReqForm, keyToFind) {
    var inp = $("#" + idofReqForm + " :input");
    var rObject = {};
    for (var i = 0; i < inp.length; i++) {

        if (inp[i]['name'].substr(inp[i]['name'].length - 2) == "[]") {
            var tmp = inp[i]['name'].substr(0, inp[i]['name'].length - 2);
            if (Array.isArray(rObject[tmp])) {
                if (inp[i]['name'].includes(keyToFind))
                    rObject[tmp].push(inp[i]['value']);
            } else {

                rObject[tmp] = [];
                rObject[tmp].push(inp[i]['value']);
            }
        } else {
            if (inp[i]['name'].includes(keyToFind))
                rObject[inp[i]['name'].replace(keyToFind, "")] = inp[i]['value'];
        }
    }


    return JSON.stringify(rObject);
}

function CheckIsFullInputes() {
    var checkedInputs = true;
    var inp = $("#" + idofReqForm + " :input");
    var rObject = {};
    for (var i = 0; i < inp.length; i++) {

        if (inp[i]['name'].substr(inp[i]['name'].length - 2) == "[]") {
            var tmp = inp[i]['name'].substr(0, inp[i]['name'].length - 2);
            if (Array.isArray(rObject[tmp])) {
                if (inp[i]['name'].includes(keyToFind, "")) {

                    if (inp[i]['value'].replace(" ", "") == "")
                        checkedInputs = false;
                }
            } else {

                if (inp[i]['name'].includes(keyToFind, "")) {

                    if (inp[i]['value'].replace(" ", "") == "")
                        checkedInputs = false;
                }
            }
        } else {
            if (inp[i]['name'].includes(keyToFind))
                if (inp[i]['name'].includes(keyToFind, "")) {

                    if (inp[i]['value'].replace(" ", "") == "")
                        checkedInputs = false;
                }
        }
    }
    return checkedInputs;
}