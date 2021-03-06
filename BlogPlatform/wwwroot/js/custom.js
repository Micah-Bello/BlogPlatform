let index = 0;

function AddTag() {
    var tagEntry = document.getElementById("TagEntry");

    let validationResult = Validate(tagEntry.value);

    if (validationResult != null) {
        swalWithDarkButton.fire({
            html: `<span class='font-weight-bolder'>${validationResult.toUpperCase()}</span>`
        });
    }
    else {
        let newOption = new Option(tagEntry.value, tagEntry.value);
        document.getElementById("TagList").options[index++] = newOption;
    }

    tagEntry.value = "";
    return true;
}

function DeleteTag() {
    let tagCount = 1;
    let tagList = document.getElementById("TagList");

    if (!tagList) return false;

    if (tagList.selectedIndex == -1) {
        swalWithDarkButton.fire({
            html: "<span class='font-weight-bolder'>SELECT A TAG TO DELETE</span>"
        });
        return true;
    }

    while (tagCount > 0) {
        
        if (tagList.selectedIndex >= 0) {
            tagList.options[tagList.selectedIndex] = null;
            --tagCount;
        }
        else {
            tagCount = 0;
        }
        index--;
    }
}

$("form").on("submit", function () {
    $("#TagList option").prop("selected", "selected");
})

if (tagValues != '') {
    let tagArray = tagValues.split(",");
    for (let i = 0; i < tagArray.length; i++) {
        ReplaceTag(tagArray[i], i);
        index++;
    }
}

function ReplaceTag(tag, index) {
    let newOption = new Option(tag, tag);
    document.getElementById("TagList").options[index++] = newOption;
}

function Validate(str) {
    if (str == "") {
        return 'Empty tags are not permitted';
    }

    var tagsSelectList = document.getElementById("TagList");

    if (tagsSelectList) {
        let options = tagsSelectList.options;

        for (let i = 0; i < options.length; i++) {
            if (options[i].value == str) {
                return 'Duplicate tags are not permitted';
            }
        }
    }
}

const swalWithDarkButton = Swal.mixin({
    customClass: {
        confirmButton: 'btn btn-danger btn-sm w-75 btn-outline-dark'
    },
    imageUrl: '/assets/img/oops.jpg',
    timer: 30000,
    buttonsStyling: false
});