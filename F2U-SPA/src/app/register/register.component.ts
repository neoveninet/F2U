import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { AuthService } from '../_services/auth.service';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css'],
})
export class RegisterComponent implements OnInit {
  // @Input() valuesFmHome: any;
  @Output() cancelRegister = new EventEmitter();
  model: any = {};
  constructor(private authService: AuthService) {}

  ngOnInit(): void {}
  register() {
    this.authService.register(this.model).subscribe(
      (next) => {
        console.log('Successfully registered as ' + this.model);
      },
      (error) => {
        console.log('Failed to login. Error: ' + error);
      }
    );
    console.log(this.model.username);
  }
  cancel() {
    this.cancelRegister.emit(false);
  }
}
