amortizationForm.addEventListener('submit', function (e) {
    e.preventDefault();

    const installmentsTable = document.querySelector('table > tbody');
    const firstNameSpan = document.querySelector("#firstName");
    const lastNameSpan = document.querySelector("#lastName");
    const emailSpan = document.querySelector("#email");
    const phoneSpan = document.querySelector("#phone");
    const dateOpenedSpan = document.querySelector("#dateOpened");
    const principalSpan = document.querySelector("#principal");
    const interestRateSpan = document.querySelector("#interestRate");
    const durationInDaysSpan = document.querySelector("#durationInDays");
    const numberOfPaymentsSpan = document.querySelector("#numberOfPayments");
    const amountPerInstallmentSpan = document.querySelector("#amountPerInstallment");
    const form = document.querySelector("#form");
    const preview = document.querySelector("#preview");
    const backButtonForPreview = document.querySelector('#backButtonForPreview');

    const firstName = amortizationForm["AmortizationViewModel.FirstName"].value;
    const lastName = amortizationForm["AmortizationViewModel.LastName"].value;
    const email = amortizationForm["AmortizationViewModel.Email"].value;
    const phone = amortizationForm["AmortizationViewModel.Phone"].value;
    const principal = amortizationForm["AmortizationViewModel.Principal"].value ?? 0;
    const interestRate = amortizationForm["AmortizationViewModel.InterestRate"].value ?? 0;
    const durationInDays = amortizationForm["AmortizationViewModel.DurationInDays"].value ?? 0;
    const dailyInterest = (1 / 30) * principal * (interestRate * 0.01);
    const interestAccrued = dailyInterest * durationInDays;
    const amountDue = parseFloat(interestAccrued) + parseFloat(principal);
    const dateOpened = amortizationForm["AmortizationViewModel.DateOpened"].value;
    let amountPerInstallment = parseFloat(amortizationForm["AmortizationViewModel.AmountPerInstallment"].value ?? 0);
    let numberOfInstallments = amortizationForm["AmortizationViewModel.NumberOfInstallments"].value ?? 0;

    const daysBetweenPayments = durationInDays / numberOfInstallments;
    let daysSinceDateOpened = 0;
    let numberOfInstallmentsCreated = 0;
    let runningBalance = amountDue;

    if (principal && durationInDays && interestRate && amountPerInstallment && numberOfInstallments) {

        installmentsTable.innerHTML = "";
        do {

            let tableRow = ``;
            let balanceBeforePayment;
            let balanceAfterPayment;

            daysSinceDateOpened = daysSinceDateOpened + daysBetweenPayments;
            numberOfInstallmentsCreated = numberOfInstallmentsCreated + 1;
            interestPayablePerInstallment = Math.floor(interestAccrued / numberOfInstallments);
            amountPayablePerInstallment = Math.floor(amountDue / numberOfInstallments);
            cummulativeInterest = interestPayablePerInstallment * numberOfInstallmentsCreated;

            if (amountPerInstallment < runningBalance) {
                balanceBeforePayment = Math.ceil(runningBalance);
                balanceAfterPayment = Math.ceil(balanceBeforePayment - amountPerInstallment);
            } else {
                balanceBeforePayment = Math.ceil(runningBalance);
                amountPerInstallment = Math.ceil(balanceBeforePayment);
                balanceAfterPayment = 0;
            }

            runningBalance = balanceAfterPayment;

            tableRow += `<tr><td>${numberOfInstallmentsCreated}</td><td>${addDaysToDate(daysSinceDateOpened, new Date(dateOpened)).toDateString()}</td><td>${amountPerInstallment}</td><td>${interestPayablePerInstallment}</td><td>${amountPayablePerInstallment}</td><td>${balanceBeforePayment}</td><td>${balanceAfterPayment}</td><td>${cummulativeInterest}</td></tr>`;
            installmentsTable.innerHTML += tableRow;
            tableRow = ``;

        } while (runningBalance > 0);

        firstNameSpan.innerHTML = firstName;
        lastNameSpan.innerHTML = lastName;
        emailSpan.innerHTML = email;
        phoneSpan.innerHTML = phone;
        dateOpenedSpan.innerHTML = dateOpened;
        principalSpan.innerHTML = principal;
        interestRateSpan.innerHTML = interestRate;
        durationInDaysSpan.innerHTML = durationInDays;
        numberOfPaymentsSpan.innerHTML = numberOfInstallments;
        amountPerInstallmentSpan.innerHTML = amountPerInstallment;

        form.classList.toggle('d-none');
        preview.classList.toggle('d-none');
        backButtonForPreview.classList.toggle('d-none')

    }
});

function addDaysToDate(days, date) {
    return new Date(date.setDate(date.getDate() + days));
}

const sendEmailButton = document.querySelector("#sendEmail");
sendEmailButton.addEventListener('click', e => {
    document.querySelector("#previewCardActions").classList.toggle('d-none');
    const emailForm = document.querySelector("#emailForm");
    const email = amortizationForm["AmortizationViewModel.Email"].value;
    emailForm['email'].value = email;
    emailForm.classList.toggle('d-none');
});

const printButton = document.querySelector("#print");
window.addEventListener('beforeprint', e => {
    const pageContent = document.getElementById('pageContent');
    const printableContent = document.getElementById('preview');
    pageContent.innerHTML = '';
    document.body.innerHTML = '';
    pageContent.appendChild(printableContent);
    document.body.appendChild(pageContent);
    const printHeader = document.getElementById('printHeader');
    printHeader.innerHTML = 'Amortization - Loan Payments Manager';
    const printFooter = document.getElementById('printFooter');
    printFooter.parentElement.removeChild(printFooter);
    Window.sizeToContent();
});

printButton.addEventListener('click', e => {
    window.print();
});

window.addEventListener('afterprint', e => {
    window.location.reload();
});