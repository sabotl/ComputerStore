import axios from 'axios';

class ApiClient {
    constructor(baseUrl) {
        this.baseUrl = baseUrl;
    }

    async getAllProducts() {
        try {
            const response = await axios.get(`${this.baseUrl}/Goods`);
            return response.data;
        } catch (error) {
            console.error('Error fetching products:', error);
            throw error;
        }
    }
}

export default ApiClient;