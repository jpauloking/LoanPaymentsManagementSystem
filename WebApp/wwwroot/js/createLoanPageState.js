function createLoanSetPageState() {
    const allFormsChildElements = document.forms[0];
    const borrowerId = allFormsChildElements['LoanCreateViewModel.BorrowerId'].value;
    const dateOpened = allFormsChildElements['LoanCreateViewModel.Loan.DateOpened'].value;
    const principal = allFormsChildElements['LoanCreateViewModel.Loan.Principal'].value;
    const durationInDays = allFormsChildElements['LoanCreateViewModel.Loan.DurationInDays'].value;
    const interestRate = allFormsChildElements['LoanCreateViewModel.Loan.InterestRate'].value;
    const penaltyFeePerDayOverDue = allFormsChildElements['LoanCreateViewModel.Loan.PercentagePenaltyFeePerDayOverDue'].value;
    const numberOfInstallments = allFormsChildElements['LoanCreateViewModel.Loan.NumberOfInstallments'].value;
    const amountPerInstallment = allFormsChildElements['LoanCreateViewModel.Loan.AmountPerInstallment'].value;
    const borrower = {
        borrowerId,
        dateOpened,
        principal,
        durationInDays,
        interestRate,
        amountPerInstallment,
        numberOfInstallments,
        penaltyFeePerDayOverDue,
    };
    const borrowerFromLocalStorage = JSON.parse(localStorage.getItem('borrower'));
    if (borrowerFromLocalStorage) {
        localStorage.removeItem('borrower');
    }
    localStorage.setItem('borrower', JSON.stringify(borrower));

    console.log('Page state saved to local storage.');
}

function createLoanGetPageState() {
    const allFormsChildElements = document.forms[0];
    const borrowerFromLocalStorage = JSON.parse(localStorage.getItem('borrower'));
    if (borrowerFromLocalStorage) {
        allFormsChildElements['LoanCreateViewModel.BorrowerId'].value = borrowerFromLocalStorage.borrowerId;
        allFormsChildElements['LoanCreateViewModel.Loan.DateOpened'].Date = borrowerFromLocalStorage.dateOpened;
        allFormsChildElements['LoanCreateViewModel.Loan.Principal'].value = borrowerFromLocalStorage.principal;
        allFormsChildElements['LoanCreateViewModel.Loan.DurationInDays'].value = borrowerFromLocalStorage.durationInDays;
        allFormsChildElements['LoanCreateViewModel.Loan.InterestRate'].value = borrowerFromLocalStorage.interestRate;
        allFormsChildElements['LoanCreateViewModel.Loan.AmountPerInstallment'].value = borrowerFromLocalStorage.amountPerInstallment;
        allFormsChildElements['LoanCreateViewModel.Loan.NumberOfInstallments'].value = borrowerFromLocalStorage.numberOfInstallments;
        allFormsChildElements['LoanCreateViewModel.Loan.PercentagePenaltyFeePerDayOverDue'].value = borrowerFromLocalStorage.penaltyFeePerDayOverDue;
        localStorage.removeItem('borrower');
    }

    console.log('Page state loaded from local storage');
}

function assignSavePageStateTriggers() {
    const saveStateTriggers = document.querySelectorAll('.save-state-trigger');
    saveStateTriggers.forEach(trigger => {
        trigger.addEventListener('click', createLoanSetPageState)
    });
}

assignSavePageStateTriggers();

createLoanGetPageState();