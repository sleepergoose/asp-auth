import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class HttpInternalService {

    constructor(private readonly httpClient: HttpClient) { }

    getRequest<T>(url: string) {
        return this.httpClient.get<T>(url, { withCredentials: true });
    }

    postRequest<T>(url: string, payload: object) {
        return this.httpClient.post<T>(url, payload, { withCredentials: true });
    }
}
