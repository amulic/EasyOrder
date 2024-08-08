import { Component, OnInit } from '@angular/core';
import { Router, RouterLink } from '@angular/router';
import { HlmLabelDirective } from '@spartan-ng/ui-label-helm';
import {HlmIconComponent, HlmIconModule, provideIcons} from "@spartan-ng/ui-icon-helm";
import { lucideEye, lucideEyeOff } from "@ng-icons/lucide"; 
import { HlmButtonDirective } from '@spartan-ng/ui-button-helm';
import { Form, FormBuilder, FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { subscribeOn } from 'rxjs';
import { CommonModule, NgIf } from '@angular/common';
import {
  HlmAlertDescriptionDirective,
  HlmAlertDirective,
  HlmAlertIconDirective,
  HlmAlertTitleDirective,
} from '@spartan-ng/ui-alert-helm';
import ValidateForm from '../../helpers/validateForm';
import { AuthService } from '../../services/auth.service';
@Component({
  selector: 'app-signup',
  standalone: true,
  imports: [HlmLabelDirective, HlmLabelDirective, HlmIconModule, HlmIconComponent, HlmButtonDirective, RouterLink, ReactiveFormsModule, CommonModule, NgIf, 
    HlmAlertDescriptionDirective,
    HlmAlertDirective,
    HlmAlertIconDirective,
    HlmAlertTitleDirective,],
  templateUrl: './signup.component.html',
  styleUrl: './signup.component.css',
  providers: [provideIcons({lucideEye, lucideEyeOff})]
})
export class SignupComponent implements OnInit {
  type: string = "password";
  isText: boolean = false;
  eyeIcon: string = "lucideEyeOff"
  signUpForm!:FormGroup;
  showAlert: boolean = false;

  constructor(private formBuilder:FormBuilder,private auth:AuthService, private router: Router) {
  }

  ngOnInit(): void {
    this.signUpForm = this.formBuilder.group({
      name: ['', Validators.required],
      surname: ['', Validators.required],
      email: ['', Validators.required],
      username: ['', Validators.required],
      password: ['', Validators.required],
    })
  }

  onSignUp() {
    
    if(this.signUpForm.valid) {
      this.auth.signUp(this.signUpForm.value).subscribe({next:(
        res=> {
          alert(res.message);
          this.signUpForm.reset();
          this.router.navigate(['login']);
        }),error:(err=>{
        alert(err?.error.message);
      })
    })
    } else {
      
      ValidateForm.validateAllFormFields(this.signUpForm);
      this.showAlert=true;
      setTimeout(() => {
        this.showAlert = !this.showAlert
      }, 2000);
    }
  }
  
  hideShowPass() {
    this.isText = !this.isText;
    this.isText ? this.eyeIcon = "lucideEyeOff": this.eyeIcon="lucideEye";
    this.isText ? this.type = "text" :this.type = "password";
  }
}
