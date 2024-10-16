import { Component, OnInit} from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { HlmIconComponent, HlmIconModule, provideIcons } from "@spartan-ng/ui-icon-helm";
import { lucideUploadCloud } from "@ng-icons/lucide";
import { AuthService } from '../../services/auth/auth.service';
import { CommonModule, NgIf } from '@angular/common';
import ValidateForm from '../../helpers/validateForm';
import { FoodService } from '../../services/food/food.service';
import { HlmButtonDirective } from '@spartan-ng/ui-button-helm';

import { UploadService } from '../../services/upload/upload.service';
@Component({
  selector: 'app-new-item',
  standalone: true,
  imports: [ReactiveFormsModule, CommonModule, NgIf, HlmButtonDirective,HlmIconComponent, HlmIconModule],
  templateUrl: './new-item.component.html',
  styleUrl: './new-item.component.css',
  providers: [provideIcons({ lucideUploadCloud })]
})
export class NewItemComponent implements OnInit {
  newItemForm!: FormGroup;
  fileToUpload: File | null = null;
  
  constructor(private formBuilder: FormBuilder, private foodService:FoodService, private uploadService:UploadService) { 
  }
  
  ngOnInit(): void {
    this.newItemForm = this.formBuilder.group({
      name: ['', Validators.required],
      price: ['', Validators.required],
      description: ['', Validators.required],
      imageLink: ['', Validators.required],
    })
  }
  
  addItem() {
    if(this.newItemForm.valid && this.fileToUpload) {
      console.log("Form data before upload", this.newItemForm.value);
      //Upload file
      this.uploadFile(this.fileToUpload).then((fileUrl) => {
        const data = new FormData();
        for(const key in this.newItemForm.value) {
          data.append(key, this.newItemForm.value[key]);
        }
        data.set('imageLink', fileUrl);
        //Create food
        
        this.foodService.createFood(data).subscribe({next:(
          res=> {
            console.log("Successfully created!");
            this.newItemForm.reset();
            this.fileToUpload = null;
          }),error:(err=>{
            alert(err?.error.message);
          })
        })
      }).catch((err)=>{
        console.log("Error uploading file", err);
      });
      
      
    } else {
      ValidateForm.validateAllFormFields(this.newItemForm);
      // this.showAlert=true;
      // setTimeout(() => {
        //   this.showAlert = !this.showAlert
        // }, 2000);
      }
    }
    
    async uploadFile(file: File) {
      const formData = new FormData();
      formData.append('file', file);
      const presignedUrl = await fetch('https://localhost:7282/api/FileUpload/upload', {
        method: 'POST',
        body: formData
      }).then(response => response.text());
      
      await fetch(presignedUrl, {
        method: 'PUT',
        body: file,
        headers: {
          'Content-Type': file.type,
          'content-length': file.size.toString()
        }
      });
      
      return presignedUrl.split('?')[0];

    }
  
    triggerFileInput() {
      const fileInput = document.getElementById('imageLink') as HTMLInputElement;
      if (fileInput) {
        fileInput.click();
      }
    } 

    onFileChange($event: any) {
      const files = $event.target.files;
      if (files && files.length > 0) {
        this.fileToUpload = files[0];  // Store the selected file
        console.log("File selected:", this.fileToUpload);  // Debug log
      } else {
        this.fileToUpload = null;  // Handle the case where no file is selected
      }
    }
    
  }


