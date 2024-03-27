class JSONToCSVConverter {
    constructor() {
        this.csvContent = '';
    }

    convertToCSV(jsonData) {
        if (typeof jsonData != 'object' || Array.isArray(jsonData)) {
            throw new Error('Invalid JSON data.');
        }
        const keys = Object.keys(jsonData);
        const headers = ['key'].concat(Object.keys(jsonData[keys[0]]));
        this.csvContent = headers.join(',') + '\n';
        keys.forEach((key) => {
            const values = [key]
            headers.forEach((header) => {
                if (jsonData[key][header]) values.push(jsonData[key][header])
            })
            this.csvContent += values.join(',') + '\n';
        });

        return this.csvContent;
    }

    // Download the generated CSV file
    downloadCSV(filename) {
        const blob = new Blob([this.csvContent], { type: 'text/csv;charset=utf-8' });
        const link = document.createElement('a');
        link.href = window.URL.createObjectURL(blob);
        link.setAttribute('download', filename);
        document.body.appendChild(link);
        link.click();
        document.body.removeChild(link);
    }
}

export default JSONToCSVConverter;