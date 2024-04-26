console.log("Loaded ... ");
let isModifiedAmountPerInstallment = false;
let isModifiedNumberOfInstallments = false;
let isModifiedPrincipal = false;
const allFormsChildElements = document.forms[0];

//console.log("Here is what you are looking for: ", allFormsChildElements);


allFormsChildElements["LoanCreateViewModel.Loan.Principal"].addEventListener("change", () => isModifiedPrincipal = true);

allFormsChildElements["LoanCreateViewModel.Loan.DurationInDays"].addEventListener("change", () => isModifiedPrincipal = true);

allFormsChildElements["LoanCreateViewModel.Loan.InterestRate"].addEventListener("change", () => isModifiedPrincipal = true);

allFormsChildElements["LoanCreateViewModel.Loan.AmountPerInstallment"].addEventListener("change", () => isModifiedAmountPerInstallment = true);

allFormsChildElements["LoanCreateViewModel.Loan.NumberOfInstallments"].addEventListener("change", () => isModifiedNumberOfInstallments = true);

//Array.from(allFormsChildElements).map(inputElement => {
//    inputElement.addEventListener("change", () => {

//        const principal = parseFloat(allFormsChildElements["LoanCreateViewModel.Loan.Principal"].value ?? 0);
//        const interestRate = parseFloat(allFormsChildElements["LoanCreateViewModel.Loan.InterestRate"].value ?? 0);
//        const durationInDays = parseInt(allFormsChildElements["LoanCreateViewModel.Loan.DurationInDays"].value ?? 0);
//        const dailyInterest = parseFloat((1 / 30) * principal * (interestRate * 0.01));
//        const interestAccrued = parseFloat(dailyInterest * durationInDays);
//        const amountDue = parseFloat(interestAccrued) + parseFloat(principal);
//        let amountPerInstallment = parseFloat(allFormsChildElements["LoanCreateViewModel.Loan.AmountPerInstallment"].value ?? 0);
//        let numberOfInstallments = parseInt(allFormsChildElements["LoanCreateViewModel.Loan.NumberOfInstallments"].value ?? 0);

//        if (principal && principal > 0) {

//            if (isModifiedAmountPerInstallment) {
//                autocompleteNumberOfInstallments(amountPerInstallment, amountDue);
//                ismodifiedamountperinstallment = false;
//            }

//            if (isModifiedNumberOfInstallments) {
//                autocompleteAmountPerInstallment(numberOfInstallments, amountDue);
//                ismodifiednumberofinstallments = false;
//            }

//        }

//    });
//});

//function autocompleteNumberOfInstallments(amountPerInstallment, amountDue) {
//    if (amountPerInstallment && amountPerInstallment > 0) {
//        const numberOfInstallments = Math.ceil(amountDue / amountPerInstallment) ?? 0;
//        console.log("Number of installments: ", numberOfInstallments);
//        allFormsChildElements["LoanCreateViewModel.Loan.NumberOfInstallments"].value = numberOfInstallments;
//    }
//}

//function autocompleteAmountPerInstallment(numberOfInstallments, amountDue) {
//    if (numberOfInstallments && numberOfInstallments > 0) {
//        const amountPerInstallment = Math.ceil(amountDue / numberOfInstallments) ?? 0;
//        console.log("Amount per installment: ", amountPerInstallment);
//        allFormsChildElements["LoanCreateViewModel.Loan.AmountPerInstallment"].value = amountPerInstallment;
//    }
//}