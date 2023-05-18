import {Component, OnInit} from '@angular/core';
import {Router} from "@angular/router";
import {Fuel} from "../../../models/fuel/Fuel";
import {FormBuilder, FormGroup, Validators} from "@angular/forms";
import {ToastrService} from "ngx-toastr";
import {FuelService} from "../../../services/fuel/fuel-service.service";

@Component({
  selector: 'app-update-fuel',
  templateUrl: './update-fuel.component.html',
  styleUrls: ['./update-fuel.component.css']
})
export class UpdateFuelComponent implements OnInit{
  editFuelForm!: FormGroup;
  fuelToEdit!: Fuel;
  submitted = false;

  constructor(private formBuilder: FormBuilder,
              private toast: ToastrService,
              private fuelService: FuelService,
              private router: Router) {}


  ngOnInit(): void {
    this.fuelToEdit = history.state;

    this.editFuelForm = this.formBuilder.group({
      id: this.fuelToEdit.id,
      newPrice: ['', [Validators.required ,Validators.min(0)]]
    });
  }

  // convenience getter for easy access to form fields
  get f() {
    return this.editFuelForm.controls;
  }

  onSubmit() {
    this.submitted = true;

    // stop here if form is invalid
    if (this.editFuelForm.invalid) {
      this.toast.warning('The update fuel form should be valid!', 'Validation form warning!');

      return;
    } else {
      this.fuelService.Update(this.editFuelForm.value)
        .subscribe(res =>
          this.toast.success('The price of fuel has been successfully updated'));
      this.router.navigate(['/fuels'], {
        queryParams: {
          duration: 2000
        }
      });
    }
  }
}
