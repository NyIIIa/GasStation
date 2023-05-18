import {Injectable} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {ToastrService} from "ngx-toastr";
import {JwtHelperService} from "@auth0/angular-jwt";
import {Router} from "@angular/router";

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private titleJwtToken = 'jwt-token';
  private url = "https://localhost:7187";

  constructor(private httpClient: HttpClient,
              private toast: ToastrService,
              private router: Router) {
    }

  Register(user: any) {
    return this.httpClient.post<any>(`${this.url}/register`, user);
  }

  Login(user: any) {
    return this.httpClient.post<any>(`${this.url}/login`, user);
  }

  setJwtTokenInLocalStorage(jwtToken: string) {
    localStorage.setItem(this.titleJwtToken, jwtToken)
  }

  getJwtTokenFromLocalStorage() {
    return localStorage.getItem(this.titleJwtToken);
  }

  isLoggedIn() {
    return !!localStorage.getItem(this.titleJwtToken);
  }
  signOut() {
    localStorage.removeItem(this.titleJwtToken);
    this.toast.success('You have been successfully logged out!');
    this.router.navigate(['/']);
  }
  decodedJwtToken() {
    const jwtHelper = new JwtHelperService();
    const jwtToken = this.getJwtTokenFromLocalStorage()!;
    return jwtHelper.decodeToken(jwtToken);
  }
}
