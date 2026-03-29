function initCategoryCascade(categorySelectId, subCategorySelectId) {
    const catSelect = document.getElementById(categorySelectId);
    const subCatSelect = document.getElementById(subCategorySelectId);

    if (!catSelect || !subCatSelect) return;

    const allSubOptions = Array.from(subCatSelect.options);

    function filterSubCategories() {
        const selectedCat = catSelect.value;
        const currentSubVal = subCatSelect.value;

        subCatSelect.innerHTML = '';

        const defaultOption = allSubOptions.find(opt => opt.value === "");
        if (defaultOption) {
            subCatSelect.appendChild(defaultOption);
        }

        const filteredOptions = allSubOptions.filter(opt => {
            return opt.value !== "" && (!selectedCat || opt.dataset.category === selectedCat);
        });

        filteredOptions.forEach(opt => subCatSelect.appendChild(opt));

        if (filteredOptions.some(opt => opt.value === currentSubVal)) {
            subCatSelect.value = currentSubVal;
        } else {
            subCatSelect.value = '';
        }
    }

    catSelect.addEventListener('change', filterSubCategories);

    filterSubCategories();
}