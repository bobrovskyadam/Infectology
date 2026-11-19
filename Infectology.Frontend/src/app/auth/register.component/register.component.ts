
import { ChangeDetectorRef, Component, inject, isDevMode, ElementRef, ViewChild, signal } from '@angular/core';
import {
  FormBuilder,
  FormGroup,
  ReactiveFormsModule,
  Validators,
  AbstractControl,
  ValidationErrors
} from '@angular/forms';
import { Router } from '@angular/router';
import { RegisterRequest } from '../../models/register-request.model';
import { AuthService } from '../../services/auth.service';
import { HttpErrorResponse } from '@angular/common/http';

import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { MatButtonModule } from '@angular/material/button';
import { MatSelectModule } from '@angular/material/select';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatIconModule } from '@angular/material/icon';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [
    ReactiveFormsModule,
    MatFormFieldModule,
    MatInputModule,
    MatDatepickerModule,
    MatNativeDateModule,
    MatButtonModule,
    MatSelectModule,
    MatCheckboxModule,
    MatIconModule
  ],
  templateUrl: './register.component.html',
  styleUrl: './register.component.scss'
})
export class RegisterComponent {
  private router = inject(Router);
  private authService = inject(AuthService);
  private cdr = inject(ChangeDetectorRef);

  isSubmitting = false;
  hidePassword = signal(true);
  hideConfirmPassword = signal(true);
  registerStep = 1;
  registerError = "";
  showStep4Errors = false;

  readonly currentYear = new Date().getFullYear();

  registerForm: FormGroup;

  // Phone prefixes
  phonePrefixes = [
    { code: '+421', label: '+421' },
    { code: '+420', label: '+420' },
    { code: '+43', label: '+43' },
    { code: '+49', label: '+49' }
  ];

  readonly steps = [
  { number: 1, label: 'Osobné údaje' },
  { number: 2, label: 'Kontakt' },
  { number: 3, label: 'Pracovisko' },
  { number: 4, label: 'Vzdelanie' },
  ] as const;

  @ViewChild('stepHeading')
  private stepHeading?: ElementRef<HTMLElement>;

  private showStep(step: number): void {
    this.registerStep = step;
    this.cdr.detectChanges();
    this.stepHeading?.nativeElement.focus();
  }

  showPassword(): void {
    this.hidePassword.update(hidden => !hidden);
  }

  showConfirmPassword(): void {
    this.hideConfirmPassword.update(hidden => !hidden);
  }

  constructor(private fb: FormBuilder) {

    this.registerForm = this.fb.group({

      // STEP 1 - Personal information
      titleBefore: [''],
      titleAfter: [''],
      firstName: ['', this.nonBlankRequired],
      lastName: ['', this.nonBlankRequired],
      birthDate: [null, Validators.required],

      // STEP 2 - Contact and account information
      address: ['', this.nonBlankRequired],
      zip: ['', this.nonBlankRequired],
      city: ['', this.nonBlankRequired],
      phonePrefix: ['+421'],
      phone: [''],
      email: ['', [this.nonBlankRequired, Validators.email]],
      fax: [''],
      password: ['', [this.nonBlankRequired, Validators.minLength(9), this.passwordCharacters]],
      confirmPassword: ['', this.nonBlankRequired],

      // STEP 3 - Workplace information
      workAddress: ['', this.nonBlankRequired],
      workZip: ['', this.nonBlankRequired],
      workCity: ['', this.nonBlankRequired],
      workPhonePrefix: ['+421'],
      workPhone: [''],
      workFax: [''],
      workEmail: ['', Validators.email],
      position: ['', this.nonBlankRequired],
      specialization: ['', this.nonBlankRequired],

      //STEP 4 
      graduationYear: [null, [
        Validators.required,
        Validators.min(1900),
        Validators.max(this.currentYear),
        Validators.pattern(/^\d{4}$/)
      ]],
      chamber: ['', this.nonBlankRequired],
      licenseId: ['', this.nonBlankRequired],

      agreeMembership: [false, Validators.requiredTrue],
      agreePrivacy: [false, Validators.requiredTrue],
      confirmTruth: [false, Validators.requiredTrue],
      agreeRules: [false, Validators.requiredTrue]

    }, {
      validators: this.passwordMatchValidator
    });

  }

  // Check whether passwords match
  passwordMatchValidator(
    control: AbstractControl
  ): ValidationErrors | null {

    const password = control.get('password')?.value;
    const confirmPassword = control.get('confirmPassword')?.value;

    if (!confirmPassword) {
      return null;
    }

    return password === confirmPassword
      ? null
      : { passwordMismatch: true };

  }

  // STEP 1 validation
  get isStep1Valid(): boolean {

    const controls = [
      'firstName',
      'lastName',
      'birthDate'
    ];

    return controls.every(
      name => this.registerForm.get(name)?.valid
    );

  }

  // STEP 2 validation
  get isStep2Valid(): boolean {

    const controls = [
      'address',
      'zip',
      'city',
      'phone',
      'email',
      'password',
      'confirmPassword'
    ];

    const controlsValid = controls.every(
      name => this.registerForm.get(name)?.valid
    );

    return controlsValid &&
      !this.registerForm.hasError('passwordMismatch');

  }

  // STEP 3 validation
  get isStep3Valid(): boolean {

    const controls = [
      'workAddress',
      'workZip',
      'workCity',
      'workPhone',
      'workEmail',
      'position',
      'specialization'
    ];

    return controls.every(
      name => this.registerForm.get(name)?.valid
    );

  }

  //STEP 4 valid
  get step4Valid(): boolean {
    return this.registerForm.get('agreeMembership')?.valid === true &&
      this.registerForm.get('agreePrivacy')?.valid === true &&
        this.registerForm.get('confirmTruth')?.valid === true &&
        this.registerForm.get('agreeRules')?.valid === true;
  }

  nextStep(): void {

    if (this.registerStep === 1 && !this.isStep1Valid) {
      for (const name of ['firstName', 'lastName', 'birthDate']) {
        this.registerForm.get(name)?.markAsTouched();
      }
      return;
    }

    if (this.registerStep === 2 && !this.isStep2Valid) {
      for (const name of [
        'address', 'zip', 'city', 'email', 'password', 'confirmPassword'
      ]) {
        this.registerForm.get(name)?.markAsTouched();
      }
      return;
    }

    if (this.registerStep === 3 && !this.isStep3Valid) {
      for (const name of [
        'workAddress', 'workZip', 'workCity',
        'workEmail', 'position', 'specialization'
      ]) {
        this.registerForm.get(name)?.markAsTouched();
      }
      return;
    }

    if (this.registerStep < 4) {
      this.showStep(this.registerStep + 1);
    }

  }

  private buildRegisterRequest(): RegisterRequest {
    const value = this.registerForm.getRawValue();
    const birthDate = value.birthDate as Date;

    const pad = (number: number) => String(number).padStart(2, '0');
    const optional = (text: string): string | null => text.trim() || null;

    const phone = value.phone.trim();
    const workPhone = value.workPhone.trim();

    return {
      firstName: value.firstName.trim(),
      lastName: value.lastName.trim(),
      dateOfBirth: `${birthDate.getFullYear()}-${pad(birthDate.getMonth() + 1)}-${pad(birthDate.getDate())}`,
      titleBefore: optional(value.titleBefore),
      titleAfter: optional(value.titleAfter),
      email: value.email.trim(),
      phoneNumber: phone ? `${value.phonePrefix} ${phone}` : null,
      mobileNumber: null,
      personalCity: value.city.trim(),
      personalAddress: value.address.trim(),
      personalPostalCode: value.zip.trim(),
      fax: optional(value.fax),
      workplaceAddress: value.workAddress.trim(),
      workplacePostalCode: value.workZip.trim(),
      workplaceCity: value.workCity.trim(),
      workplacePhone: workPhone ? `${value.workPhonePrefix} ${workPhone}` : null,
      workplaceEmail: optional(value.workEmail),
      workplaceFax: optional(value.workFax),
      workplaceFunction: value.position.trim(),
      focus: value.specialization.trim(),
      graduationYear: Number(value.graduationYear),
      chamber: value.chamber.trim(),
      chamberId: value.licenseId.trim(),
      password: value.password,
    };
  }

  previousStep(): void {

    if (this.registerStep > 1) {
      this.showStep(this.registerStep - 1);
    }

  }

  get isRegisterValid(): boolean {
    return this.registerForm.valid;
  }

  goToLogin(): void {
    this.router.navigate(['/login']);
  }

  submit(): void {
    if (this.isSubmitting) return;

    if (!this.isRegisterValid) {
      this.showStep4Errors = true;
      this.registerForm.markAllAsTouched();

      if (!this.isStep1Valid) this.showStep(1);
        else if (!this.isStep2Valid) this.showStep(2);
        else if (!this.isStep3Valid) this.showStep(3);
        else this.showStep(4);

      return;
    }

    this.registerError = '';
    this.isSubmitting = true;

    this.authService.register(this.buildRegisterRequest()).subscribe({
      next: () => {
        this.isSubmitting = false;
        this.showStep(5);
      },
      error: (error: HttpErrorResponse) => {
        this.isSubmitting = false;

        if (error.status === 409) {
          this.registerError = 'Účet s týmto e-mailom už existuje.';
        } else if (error.status === 400) {
          this.registerError = 'Skontroluj vyplnené údaje.';
        } else if (error.status === 0) {
          this.registerError = 'Nepodarilo sa pripojiť k serveru.';
        } else {
          this.registerError = 'Registrácia sa nepodarila. Skús to znova.';
        }

        this.cdr.markForCheck();
      },
    });
  }

  private nonBlankRequired(control: AbstractControl): ValidationErrors | null {
    return typeof control.value === 'string' && control.value.trim().length > 0
      ? null
      : { required: true };
  }

  private passwordCharacters(control: AbstractControl): ValidationErrors | null {
    const value = control.value;

    if (typeof value !== 'string' || !value.trim()) return null;

    const errors: ValidationErrors = {};
    if (!/\p{Ll}/u.test(value)) errors['lowercase'] = true;
    if (!/\p{Lu}/u.test(value)) errors['uppercase'] = true;
    if (!/[0-9]/.test(value)) errors['digit'] = true;

    return Object.keys(errors).length ? errors : null;
  }

  readonly showTestDataButton = isDevMode();

  fillTestData(): void {
    this.registerForm.patchValue({
      firstName: 'Test',
      lastName: 'Lekár',
      birthDate: new Date(1990, 0, 15),
      address: 'Testovacia 1',
      zip: '04001',
      city: 'Košice',
      email: `test+${Date.now()}@example.com`,
      password: 'TestHeslo123!',
      confirmPassword: 'TestHeslo123!',
      workAddress: 'Nemocničná 5',
      workZip: '04001',
      workCity: 'Košice',
      position: 'Lekár',
      specialization: 'Infektológia',
      graduationYear: 2015,
      chamber: 'SLK',
      licenseId: 'TEST-12345',
      agreeMembership: true,
      agreePrivacy: true,
      confirmTruth: true,
      agreeRules: true,
    });

    this.registerError = '';
    this.showStep(4);
  }
}
