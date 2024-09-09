import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class UploadService {
  url = 'https://localhost:7282/api/FileUpload';
  constructor(private http: HttpClient) { }

  upload(formData: FormData) {
    return this.http.post(`${this.url}/upload`, formData);
  }
}
