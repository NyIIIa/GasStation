import {Component, OnInit} from '@angular/core';
import {FormBuilder, FormControl, FormGroup, Validators} from "@angular/forms";
import {AuthService} from "../../services/authentication/auth.service";
import {ToastrService} from "ngx-toastr";
import {Router} from "@angular/router";
import {UserStoreService} from "../../services/userstore/user-store.service";
import {Role} from "../../models/enums/Role";

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})

export class LoginComponent implements OnInit {
  loginForm!: FormGroup;
  submitted = false;

  constructor(private formBuilder: FormBuilder,
              private toast: ToastrService,
              private authService: AuthService,
              private userStoreService: UserStoreService,
              private router: Router) {

  }

  ngOnInit() {
    this.loginForm = this.formBuilder.group({
      login: ['', [Validators.required, Validators.minLength(5), Validators.maxLength(20)]],
      password: ['', [Validators.required, Validators.minLength(5), Validators.maxLength(20)]]
    });
  }

  // convenience getter for easy access to form fields
  get f() {
    return this.loginForm.controls;
  }

  onSubmit() {
    this.submitted = true;

    // stop here if form is invalid
    if (this.loginForm.invalid) {
      this.toast.warning('The authorization form should be valid!', 'Validation form warning!');

      return;
    } else {
      this.authService.Login(this.loginForm.value)
        .subscribe(res => {
          this.loginForm.reset();
          this.authService.setJwtTokenInLocalStorage(res.token);
          let userPayload = this.authService.decodedJwtToken();
          this.userStoreService.setRoleForStore(Role[userPayload.Role as keyof typeof Role]);
          this.toast.success('You have been successfully authenticated!', 'Authentication information!');
          this.router.navigate(['/']);
        });
    }
  }
}
