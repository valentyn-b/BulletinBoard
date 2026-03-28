function initCategoryCascade(categorySelectId, subCategorySelectId, rulesMap) {
    const catSelect = document.getElementById(categorySelectId);
    const subCatSelect = document.getElementById(subCategorySelectId);

    if (!catSelect || !subCatSelect || !rulesMap) return;

    let currentSubVal = subCatSelect.value;

    const allSubOptions = Array.from(subCatSelect.options).map(o => o.cloneNode(true));

    function updateSubCategories() {
        subCatSelect.innerHTML = '';
        const selectedCat = catSelect.value;

        if (!selectedCat) {
            allSubOptions.forEach(opt => subCatSelect.appendChild(opt.cloneNode(true)));
        } else {
            const defaultOpt = allSubOptions.find(o => o.value === "");
            if (defaultOpt) subCatSelect.appendChild(defaultOpt.cloneNode(true));

            if (rulesMap[selectedCat]) {
                const allowed = rulesMap[selectedCat];
                allSubOptions.forEach(opt => {
                    if (opt.value && allowed.includes(parseInt(opt.value))) {
                        subCatSelect.appendChild(opt.cloneNode(true));
                    }
                });
            }
        }

        if (currentSubVal && Array.from(subCatSelect.options).some(o => o.value === currentSubVal)) {
            subCatSelect.value = currentSubVal;
        } else {
            subCatSelect.value = '';
        }
    }

    catSelect.addEventListener('change', () => {
        currentSubVal = '';
        updateSubCategories();
    });

    updateSubCategories();
}