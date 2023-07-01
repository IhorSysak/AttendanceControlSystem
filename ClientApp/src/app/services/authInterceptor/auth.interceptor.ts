import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Router } from "@angular/router";
import { Observable } from "rxjs";

@Injectable()
export class AuthInterceptor implements HttpInterceptor {
    
    constructor(private router: Router) {}
    
    intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        const token = localStorage.getItem('authToken');
        
        if (token) {
            req = req.clone({
                setHeaders: { Authorization: `Bearer ${token}` }
            });
        }
        else {
            this.router.navigate(['login']);
        }
        return next.handle(req);
    }
}