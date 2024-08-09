import { Component, OnInit } from '@angular/core';
import { Router, RouterLink } from '@angular/router';
import { HlmButtonDirective } from '@spartan-ng/ui-button-helm';
import { HlmLabelDirective } from '@spartan-ng/ui-label-helm';
import { HlmIconComponent, HlmIconModule, provideIcons } from "@spartan-ng/ui-icon-helm";
import { lucideAlertTriangle, lucideEye, lucideEyeOff } from "@ng-icons/lucide"; 
import { Form, FormBuilder, FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { CommonModule, NgIf } from '@angular/common';
import {
  HlmAlertDescriptionDirective,
  HlmAlertDirective,
  HlmAlertIconDirective,
  HlmAlertTitleDirective,
} from '@spartan-ng/ui-alert-helm';
import ValidateForm from '../../helpers/validateForm';
import { AuthService } from '../../services/auth/auth.service';
import { ToastModule } from 'primeng/toast';
import { MessageService } from 'primeng/api';



//import { HlmInputDirective } from '@spartan-ng/ui-input-helm';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [RouterLink, HlmButtonDirective, HlmLabelDirective, HlmIconModule, HlmIconComponent, ReactiveFormsModule, CommonModule, NgIf,
    HlmAlertDescriptionDirective,
    HlmAlertDirective,
    HlmAlertIconDirective,
    HlmAlertTitleDirective,
    ToastModule
  ],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css',
  providers: [provideIcons({lucideEye, lucideEyeOff, lucideAlertTriangle}), MessageService]
})
export class LoginComponent implements OnInit {
  
  type: string = "password";
  isText: boolean = false;
  eyeIcon: string = "lucideEyeOff"
  loginForm!: FormGroup;
  showAlert: boolean = false;
  
  constructor(private formBuilder: FormBuilder, private auth:AuthService, private router:Router, private messageService: MessageService) {
  }
  
  ngOnInit(): void {
    this.loginForm = this.formBuilder.group({
      username: ['', Validators.required],
      password: ['', Validators.required]
    })
  }
  
  onSubmit() {
    if(this.loginForm.valid) {
      this.auth.signIn(this.loginForm.value).subscribe((response) => {
        this.loginForm.reset();
        this.auth.storeToken(response.token);
        alert(response.message);
        this.messageService.add({severity: 'success', summary:  'Logged in successfully!', detail: response.message });
        this.router.navigate(['home'])
      }, (err) => {
        alert(err?.error.message);
      })
    } else {
      ValidateForm.validateAllFormFields(this.loginForm);
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
