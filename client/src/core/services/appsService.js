const APPS_DATA_KEY = "apps_data";

export const getAppsData = () => {
    const apps_data = String(window.localStorage.getItem(APPS_DATA_KEY))
    if (!apps_data) return null
    return JSON.parse(apps_data)
};

export const saveAppsData = (app_arr) => {
    let current_values = getAppsData()
    if (!current_values) current_values = {};

    current_values['apps_arr'] = app_arr || current_values['apps_arr'] || {}

    window.localStorage.setItem(APPS_DATA_KEY, JSON.stringify(current_values));
    return current_values
};

export const destroyData = () => {
    window.localStorage.removeItem(APPS_DATA_KEY);
};

export default { getAppsData, saveAppsData, destroyData };

