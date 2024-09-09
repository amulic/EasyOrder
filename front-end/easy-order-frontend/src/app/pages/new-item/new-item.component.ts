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
      console.log(this.newItemForm.value);
      //Upload file
      this.uploadFile(this.fileToUpload).then((fileUrl) => {
        this.newItemForm.patchValue({imageLink:fileUrl});
      })
      console.log(this.newItemForm.value);
      //Create food
      this.foodService.createFood(this.newItemForm.value).subscribe({next:(
        res=> {
          console.log("Successfully created!");
          this.newItemForm.reset();
           
        }),error:(err=>{
          alert(err?.error.message);
        })
      })
    } else {
      ValidateForm.validateAllFormFields(this.newItemForm);
      // this.showAlert=true;
      // setTimeout(() => {
        //   this.showAlert = !this.showAlert
        // }, 2000);
      }
    }
    
    uploadFile(file: File): Promise<string> {
     return new Promise((resolve, reject) => {
      const formData = new FormData();
      formData.append('image', file);

      this.uploadService.upload(formData).subscribe({
        next: (response:any) => {
          console.log("File uploaded successfully!", response);
          resolve(response.fileUrl);
        },
        error: (err) => {
          console.log("Error uploading file", err);
          reject(err);
        }
      });
     });
    }

    triggerFileInput() {
      const fileInput = document.getElementById('imageLink') as HTMLInputElement;
      if (fileInput) {
        fileInput.click();
      }
    } 

    onFileChange($event: any) {
      if ($event.target.files && $event.target.files.length > 0) {
        this.fileToUpload = $event.target.files[0];
      }
    }
    
  }


