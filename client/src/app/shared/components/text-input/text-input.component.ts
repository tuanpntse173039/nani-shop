import { CommonModule } from '@angular/common';
import { Component, ElementRef, Input, OnInit, Self, ViewChild } from '@angular/core';
import { ControlValueAccessor, NgControl } from '@angular/forms';

@Component({
  selector: 'app-text-input',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './text-input.component.html',
  styleUrl: './text-input.component.scss',
})
export class TextInputComponent implements OnInit, ControlValueAccessor {
  @ViewChild('input', { static: true }) input: ElementRef | undefined;
  @Input() inputType = 'text';
  @Input() inputLabel: string | undefined;

  private onChange?: (value: string) => void;
  private onTouched?: () => void;

  public inputValue = '';

  constructor(@Self() public controlDir: NgControl) {
    this.controlDir.valueAccessor = this;
  }

  ngOnInit(): void {
    const control = this.controlDir.control;
    const validators = control?.validator ? [control.validator] : [];
    const asyncValidators = control?.asyncValidator ? [control.asyncValidator] : [];

    control?.setValidators(validators);
    control?.setAsyncValidators(asyncValidators);
    control?.updateValueAndValidity();
  }

  writeValue(value: string): void {
    this.inputValue = value;
  }
  registerOnChange(fn: (value: string) => void): void {
    this.onChange = fn;
  }
  registerOnTouched(fn: () => void): void {
    this.onTouched = fn;
  }
  setDisabledState?(isDisabled: boolean): void {
    //Implement if needed
    console.log(isDisabled);
  }

  public onInputChange(): void {
    if (this.onChange) this.onChange(this.inputValue);
  }

  public onBlur(): void {
    if (this.onTouched) this.onTouched();
  }
}
