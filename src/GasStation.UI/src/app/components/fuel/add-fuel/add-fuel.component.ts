import {Component, OnInit} from '@angular/core';
import {Fuel} from "../../../models/fuel/Fuel";
import {FormBuilder, FormGroup, Validators} from "@angular/forms";
import {Role} from "../../../models/enums/Role";
import {ToastrService} from "ngx-toastr";
import {FuelService} from "../../../services/fuel/fuel-service.service";
import {Router} from "@angular/router";

@Component({
  selector: 'app-add-fuel',
  templateUrl: './add-fuel.component.html',
  styleUrls: ['./add-fuel.component.css']
})
export class AddFuelComponent implements OnInit {
  addFuelForm!: FormGroup;
  submitted = false;

  constructor(private formBuilder: FormBuilder,
              private toast: ToastrService,
              private fuelService: FuelService,
              private router: Router) {

  }

  ngOnInit(): void {
    this.addFuelForm = this.formBuilder.group({
      title: ['', [Validators.required, Validators.minLength(2), Validators.maxLength(10)]],
      quantity: ['', [Validators.required, Validators.min(0)]],
      price: ['', [Validators.required, Validators.min(0)]]
    });
  }

  // convenience getter for easy access to form fields
  get f() {
    return this.addFuelForm.controls;
  }

  onSubmit() {
    this.submitted = true;

    // stop here if form is invalid
    if (this.addFuelForm.invalid) {
      this.toast.warning('The add fuel form should be valid!', 'Validation form warning!');

      return;
    } else {
      this.fuelService.Add(this.addFuelForm.value)
        .subscribe(res => {
          this.toast.success('The fuel has been successfully added!');
          this.router.navigate(['/fuels']);
        });
    }
  }
}
