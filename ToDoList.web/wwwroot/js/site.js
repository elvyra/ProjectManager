// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

GetData();

//
//  Gets public data from API
//
function GetData() {
    fetch("/api/persons")
        .then(data => data.json())
        .then(data => SetTeam(data))
        .catch(err => console.log(err));

    fetch("/api/projects")
        .then(data => data.json())
        .then(data => SetProjects(data))
        .catch(err => console.log(err));

    fetch("/api/faq")
        .then(data => data.json())
        .then(data => SetFaq(data))
        .catch(err => console.log(err));
}

//
// Team section update with data
//

function SetTeam(data) {
    let slider = $("#section-team .pb_slide_testimonial_sync_v1");
    let slides = "";

    if (data != null && data.length > 0) {
        $("#menu-item-team").show();
        $("#section-team").show();
    }

    for (person of data) {
        slides += `<div>
                  <div class="media d-block text-center testimonial_v1 pb_quote_v2">
                    <div class="media-body">
                      <blockquote class="my-4 h3"><span class="quote">&ldquo;</span>${person.notes}</blockquote>
                      <img class="img-fluid rounded-circle mx-auto mb-3" src="/assets/images/person_1.jpg" alt="${person.name} ${person.surname} photo">
                      <h3 class="heading">${person.name} ${person.surname}</h3>
                      <p>
                        <i class="ion-ios-star text-warning"></i>
                        <i class="ion-ios-star text-warning"></i>
                        <i class="ion-ios-star text-warning"></i>
                        <i class="ion-ios-star-half text-warning"></i>
                        <i class="ion-ios-star-outline text-warning"></i>
                      </p>
                      <span class="subheading"><i class="fa fa-envelope mr-2"></i>${person.email}</span>
                    </div>
                  </div></div>
                `;
    }

    slider.html(slides);

    slider.slick({
        slidesToShow: 1,
        slidesToScroll: 1,
        autoplay: true,
        autoplaySpeed: 2500,
        arrows: false,
        dots: true
    });
}

//
// Portfolio section update with data
//

function SetProjects(data) {
    let list = $("#section-projects .ourPortfolio");
    let items = "";

    if (data != null && data.length > 0) {
        $("#menu-item-projects").show();
        $("#section-projects").show();
    }

    for (project of data) {
        items += `<div class="col-12 col-md-6 col-xl-4" style="padding: 0;">
                <div class="pb_pricing_v1 p-5 border text-center bg-white h-100" style="margin: 0;">
                    <h3>${(project.clientCompany) ? project.clientCompany : ""}</h3>
                    <span class="price" style="font-size: 1.8em;">${(project.title) ? project.title : ""}</span>
                    <p class="text-muted">(${(project.startDate) ? project.startDate.substr(0, 7) : ""} - ${(project.endDate) ? project.endDate.substr(0, 7) : ""})</p>
                    <p class="pb_font-15">${(project.description) ? project.description : ""}</p>                   
                </div>
            </div>`;
    }

    list.html(items);
}

//
// FAQ section update with data
//

function SetFaq(data) {
    let list = $("#section-faq .pb_accordion");
    let items = "";

    if (data != null && data.length > 0) {
        $("#menu-item-faq").show();
        $("#section-faq").show();
    }

    for (faq of data) {
        items += `<div class="item">
                        <a data-toggle="collapse" data-parent="#pb_faq" href="#pb_${faq.id}" aria-expanded="false" aria-controls="pb_${faq.id}" class="pb_font-22 py-4">${faq.question}</a>
                        <div id="pb_${faq.id}" class="collapse" role="tabpanel">
                            <div class="py-3">
                                <p>${faq.answer}</p>
                            </div>
                        </div>
                    </div>`;
    }

    list.html(items);
} 