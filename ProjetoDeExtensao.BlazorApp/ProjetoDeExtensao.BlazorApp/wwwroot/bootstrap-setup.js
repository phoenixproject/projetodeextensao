// Este arquivo deve ser incluído após o carregamento do DOM
// e após a inclusão dos scripts do Bootstrap e Popper.js

// Inicializa todos os dropdowns do Bootstrap
document.addEventListener('DOMContentLoaded', function() {
    // Inicializa todos os dropdowns
    var dropdownElementList = [].slice.call(document.querySelectorAll('.dropdown-toggle'));
    var dropdownList = dropdownElementList.map(function(dropdownToggleEl) {
        return new bootstrap.Dropdown(dropdownToggleEl);
    });

    // Log para depuração
    console.log('Bootstrap dropdowns inicializados:', dropdownList.length);
});

// Função para inicializar dropdowns manualmente (pode ser chamada após atualização do DOM)
window.initializeDropdowns = function() {
    var dropdownElementList = [].slice.call(document.querySelectorAll('.dropdown-toggle'));
    var dropdownList = dropdownElementList.map(function(dropdownToggleEl) {
        return new bootstrap.Dropdown(dropdownToggleEl);
    });
    console.log('Dropdowns inicializados manualmente:', dropdownList.length);
    return true;
};

// No arquivo JavaScript
window.setupClickOutside = function (dotNetHelper) {
    document.addEventListener('click', function (event) {
        dotNetHelper.invokeMethodAsync('CloseDropdown');
    });
}