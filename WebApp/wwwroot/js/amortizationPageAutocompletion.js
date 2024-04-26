const amortizationForm = document.forms[0];
let isModifiedAmountPerInstallment = false;
let isModifiedNumberOfInstallments = false;
let isModifiedPrincipal = false;

amortizationForm["AmortizationViewModel.Principal"].addEventListener("change", () => isModifiedPrincipal = true);

amortizationForm["AmortizationViewModel.DurationInDays"].addEventListener("change", () => isModifiedPrincipal = true);

amortizationForm["AmortizationViewModel.InterestRate"].addEventListener("change", () => isModifiedPrincipal = true);

amortizationForm["AmortizationViewModel.AmountPerInstallment"].addEventListener("change", () => isModifiedAmountPerInstallment = true);

amortizationForm["AmortizationViewModel.NumberOfInstallments"].addEventListener("change", () => isModifiedNumberOfInstallments = true);

Array.from(amortizationForm).map(inputElement => {
    inputElement.addEventListener("change", () => {

        const principal = parseFloat(amortizationForm["AmortizationViewModel.Principal"].value ?? 0);
        const interestRate = parseFloat(amortizationForm["AmortizationViewModel.InterestRate"].value ?? 0);
        const durationInDays = parseInt(amortizationForm["AmortizationViewModel.DurationInDays"].value ?? 0);
        const dailyInterest = parseFloat((1 / 30) * principal * (interestRate * 0.01));
        const interestAccrued = parseFloat(dailyInterest * durationInDays);
        const amountDue = parseFloat(interestAccrued) + parseFloat(principal);
        let amountPerInstallment = parseFloat(amortizationForm["AmortizationViewModel.AmountPerInstallment"].value ?? 0);
        let numberOfInstallments = parseInt(amortizationForm["AmortizationViewModel.NumberOfInstallments"].value ?? 0);

        if (principal && principal > 0) {

            if (isModifiedAmountPerInstallment) {
                autocompleteNumberOfInstallments(amountPerInstallment, amountDue);
                isModifiedAmountPerInstallment = false;
            }

            if (isModifiedNumberOfInstallments) {
                autocompleteAmountPerInstallment(numberOfInstallments, amountDue);
                isModifiedNumberOfInstallments = false;
            }

        }

    });
});

function autocompleteNumberOfInstallments(amountPerInstallment, amountDue) {
    if (amountPerInstallment && amountPerInstallment > 0) {
        const numberOfInstallments = Math.ceil(amountDue / amountPerInstallment) ?? 0;
        amortizationForm["AmortizationViewModel.NumberOfInstallments"].value = numberOfInstallments;
    }
}

function autocompleteAmountPerInstallment(numberOfInstallments, amountDue) {
    if (numberOfInstallments && numberOfInstallments > 0) {
        const amountPerInstallment = Math.ceil(amountDue / numberOfInstallments) ?? 0;
        amortizationForm["AmortizationViewModel.AmountPerInstallment"].value = amountPerInstallment;
    }
}