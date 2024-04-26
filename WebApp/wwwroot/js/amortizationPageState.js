function amortizationPageSetPageState() {
    const amortizationForm = document.forms[0];
    const amortizationNumber = amortizationForm["AmortizationViewModel.AmortizationNumber"].value;
    const firstName = amortizationForm["AmortizationViewModel.FirstName"].value;
    const lastName = amortizationForm["AmortizationViewModel.LastName"].value;
    const email = amortizationForm["AmortizationViewModel.Email"].value;
    const phone = amortizationForm["AmortizationViewModel.Phone"].value;
    const principal = parseFloat(amortizationForm["AmortizationViewModel.Principal"].value ?? 0);
    const interestRate = parseFloat(amortizationForm["AmortizationViewModel.InterestRate"].value ?? 0);
    const durationInDays = parseInt(amortizationForm["AmortizationViewModel.DurationInDays"].value ?? 0);
    const dateOpened = Date.parse(amortizationForm["AmortizationViewModel.DateOpened"].value);
    const numberOfInstallments = parseInt(amortizationForm["AmortizationViewModel.NumberOfInstallments"].value ?? 0);
    const amountPerInstallment = parseFloat(amortizationForm["AmortizationViewModel.AmountPerInstallment"].value ?? 0);

    amortizationPageState = {
        amortizationNumber,
        firstName,
        lastName,
        email,
        phone,
        principal,
        interestRate,
        durationInDays,
        dateOpened,
        numberOfInstallments,
        amountPerInstallment,
    };

    const amortizationPageStateFromLocalStorage = localStorage.getItem('amortizationPageState');
    if (amortizationPageStateFromLocalStorage) {
        localStorage.removeItem('amortizationPageState');
    }
    localStorage.setItem('amortizationPageState', JSON.stringify(amortizationPageState));

    console.log('Page state saved to local storage.');

}

function amortizationPageGetPageState() {
    const amortizationForm = document.forms[0];
    const amortizationPageStateFromLocalStorage = JSON.parse(localStorage.getItem('amortizationPageState'));
    if (amortizationPageStateFromLocalStorage) {

        amortizationForm["AmortizationViewModel.AmortizationNumber"].value = amortizationPageStateFromLocalStorage.amortizationNumber;
        amortizationForm["AmortizationViewModel.FirstName"].value = amortizationPageStateFromLocalStorage.firstName;
        amortizationForm["AmortizationViewModel.LastName"].value = amortizationPageStateFromLocalStorage.lastName;
        amortizationForm["AmortizationViewModel.Email"].value = amortizationPageStateFromLocalStorage.email;
        amortizationForm["AmortizationViewModel.Phone"].value = amortizationPageStateFromLocalStorage.phone;
        amortizationForm["AmortizationViewModel.Principal"].value = amortizationPageStateFromLocalStorage.principal;
        amortizationForm["AmortizationViewModel.InterestRate"].value = amortizationPageStateFromLocalStorage.interestRate;
        amortizationForm["AmortizationViewModel.DurationInDays"].value = amortizationPageStateFromLocalStorage.durationInDays;
        amortizationForm["AmortizationViewModel.DateOpened"].Date = amortizationPageStateFromLocalStorage.dateOpened;
        amortizationForm["AmortizationViewModel.NumberOfInstallments"].value = amortizationPageStateFromLocalStorage.numberOfInstallments;
        amortizationForm["AmortizationViewModel.AmountPerInstallment"].value = amortizationPageStateFromLocalStorage.amountPerInstallment;

        localStorage.removeItem('amortizationPageState');
    }

    console.log('Page state loaded from local storage');

}

document.forms[0].addEventListener('submit', e => {
    amortizationPageSetPageState();
});

amortizationPageGetPageState();
